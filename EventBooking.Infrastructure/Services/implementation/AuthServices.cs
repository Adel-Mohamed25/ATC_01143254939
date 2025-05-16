using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.IRepositories;
using EventBooking.Infrastructure.Models.Authentication;
using EventBooking.Infrastructure.Models.Email;
using EventBooking.Infrastructure.Services.Abstractions;
using EventBooking.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EventBooking.Infrastructure.Services.implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly JWTSettings _jWTSettings;
        private readonly IUnitOfWork _unitOfWork;

        public AuthServices(IUnitOfWork unitOfWork, IOptions<JWTSettings> jWTSettings)
        {
            _jWTSettings = jWTSettings.Value;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Validates the token parameters, including issuer, signature, and key validation.
        /// </summary>
        /// <param name="jwt">The token to be validated.</param>
        /// <returns>True if the token is valid; otherwise, False.</returns>
        Task<bool> IsTokenParametersValidAsync(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Secret));
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jWTSettings.ValidateIssuer,
                ValidIssuers = new[] { _jWTSettings.Issuer },
                ValidateIssuerSigningKey = _jWTSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = key,
                ValidAudience = _jWTSettings.Audience,
                ValidateAudience = _jWTSettings.ValidateAudience,
                ValidateLifetime = false,
            };
            try
            {
                var validator = handler.ValidateToken(jwt, parameters, out SecurityToken validatedJWT);
                return Task.FromResult(validatedJWT != null);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }


        /// <summary>
        /// Checks if the token uses the HmacSha256Signature signing algorithm.
        /// </summary>
        /// <param name="jwtSecurityJWT">The JWT Security Token object.</param>
        /// <returns>True if the algorithm is valid; otherwise, False.</returns>
        Task<bool> IsTokenAlgorithmValidAsync(JwtSecurityToken jwtSecurityJWT) =>
            Task.FromResult(jwtSecurityJWT.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature));


        /// <summary>
        /// Validates the token based on parameters and the signing algorithm.
        /// </summary>
        /// <param name="jwt">The token as a string.</param>
        /// <param name="jwtSecurityJWT">The JWT Security Token object.</param>
        /// <returns>True if the token is valid; otherwise, False.</returns>
        public Func<string, JwtSecurityToken, Task<bool>> IsTokenValidAsync
         => async (jwt, jwtSecurityJWT) =>
         await IsTokenParametersValidAsync(jwt) && await IsTokenAlgorithmValidAsync(jwtSecurityJWT);


        /// <summary>
        /// Retrieves a new JWT token or returns the active token for the user.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>An AuthModel containing JWT and Refresh Token.</returns>
        public async Task<AuthModel> GetTokenAsync(User user)
        {
            var authModel = new AuthModel();

            if (user.JwtTokens.Any(jwt => jwt.IsRefreshJWTActive))
            {
                var activeUserJWT = user.JwtTokens.Where(jwt => jwt.IsRefreshJWTActive).FirstOrDefault();

                authModel.JWTModel = new()
                {
                    JWT = activeUserJWT.JWT,
                    JWTExpireDate = activeUserJWT.JWTExpireDate,

                };

                authModel.RefreshJWTModel = new()
                {
                    RefreshJWT = activeUserJWT.RefreshJWT,
                    RefreshJWTExpireDate = activeUserJWT.RefreshJWTExpireDate
                };
            }
            else
            {
                var token = await GenerateTokenAsync(user, GetClaimsAsync);

                var refreshJWT = GenerateRefreshToken();

                var JwtToken = new JwtToken
                {
                    UserId = user.Id,
                    JWT = token,
                    RefreshJWT = refreshJWT,
                    JWTExpireDate = DateTime.Now.AddHours(_jWTSettings.AccessTokenExpireDate),
                    RefreshJWTExpireDate = DateTime.Now.AddDays(_jWTSettings.RefreshTokenExpireDate),
                    IsRefreshJWTUsed = true,
                    CreatedDate = DateTime.Now,
                };

                using var trasaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    await _unitOfWork.JwtTokens.CreateAsync(JwtToken);
                    await _unitOfWork.SaveChangesAsync();
                    await trasaction.CommitAsync();

                }
                catch
                {
                    await trasaction.RollbackAsync();
                }

                authModel.JWTModel = new()
                {
                    JWT = JwtToken.JWT,
                    JWTExpireDate = JwtToken.JWTExpireDate
                };

                authModel.RefreshJWTModel = new()
                {
                    RefreshJWT = JwtToken.RefreshJWT,
                    RefreshJWTExpireDate = JwtToken.RefreshJWTExpireDate
                };
            }
            return authModel;
        }


        /// <summary>
        /// Generates a random Refresh Token for re-authentication.
        /// </summary>
        /// <returns>A Base64-encoded Refresh Token.</returns>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        /// <summary>
        /// Generates a new JWT Access Token using a list of claims.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <param name="Claims">A function used to retrieve user claims.</param>
        /// <returns>A JWT Access Token as a string.</returns>
        private async Task<string> GenerateTokenAsync(User user, Func<User, Task<List<Claim>>> Claims)
        {
            var claims = await Claims.Invoke(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Secret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var jwtJWT = new JwtSecurityToken(

                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jWTSettings.AccessTokenExpireDate),
                signingCredentials: signingCredentials
                );
            var accessJWT = new JwtSecurityTokenHandler().WriteToken(jwtJWT);

            return accessJWT;
        }


        /// <summary>
        /// Retrieves a list of user claims, including roles and basic information.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>A list of Claim objects associated with the user.</returns>
        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var userRolesNames = await _unitOfWork.Users.UserManager.GetRolesAsync(user);
            var userClaims = await _unitOfWork.Users.UserManager.GetClaimsAsync(user);

            var userRoles = (await _unitOfWork.Roles.GetAllAsync(r => userRolesNames.Contains(r.Name ?? "Not Found"))).ToList();

            var permissions = new List<Claim>();
            foreach (var role in userRoles)
            {
                var roleClams = await _unitOfWork.Roles.RoleManager.GetClaimsAsync(role);
                permissions.AddRange(roleClams);
            }


            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new (ClaimTypes.PrimarySid, user.Id?.ToString() ?? throw new ArgumentNullException(nameof(user.Id))),
                new (ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new (ClaimTypes.Surname, user.LastName ?? string.Empty),
                new (ClaimTypes.Name, user.UserName ?? throw new ArgumentNullException(nameof(user.UserName))),
                new (ClaimTypes.Email, user.Email ?? throw new ArgumentNullException(nameof(user.Email))),
            };

            claims.AddRange(userRolesNames.Select(role => new Claim(ClaimTypes.Role, role)));


            claims.AddRange(userClaims);
            claims.AddRange(permissions);

            return claims;
        }


        /// <summary>
        /// Reads a JWT token from a given string and returns it as a JwtSecurityToken object.
        /// </summary>
        /// <param name="jwt">The token string.</param>
        /// <returns>A JwtSecurityToken object if valid; otherwise, null.</returns>
        public Task<JwtSecurityToken> ReadTokenAsync(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            if (string.IsNullOrEmpty(jwt) || string.IsNullOrWhiteSpace(jwt))
                return Task.FromResult<JwtSecurityToken>(null);

            return Task.FromResult(handler.ReadJwtToken(jwt));
        }


        /// <summary>
        /// Generates a new JWT Access Token using a valid Refresh Token.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>An AuthModel containing the new tokens.</returns>
        public async Task<AuthModel> GetRefreshTokenAsync(User user)
        {
            var jwtJWT = user.JwtTokens.FirstOrDefault(u => u.IsRefreshJWTActive);
            if (jwtJWT is null)
                return null;

            jwtJWT.RefreshJWTRevokedDate = DateTime.Now;

            var newJWT = await GenerateTokenAsync(user, GetClaimsAsync);
            var newRefreshJWT = GenerateRefreshToken();

            var newUserJWT = new JwtToken
            {
                Id = jwtJWT.Id,
                UserId = user.Id,
                JWT = newJWT,
                JWTExpireDate = DateTime.Now.AddHours(_jWTSettings.AccessTokenExpireDate),
                RefreshJWT = newRefreshJWT,
                RefreshJWTExpireDate = jwtJWT.RefreshJWTExpireDate,
                IsRefreshJWTUsed = true
            };

            await _unitOfWork.JwtTokens.UpdateAsync(newUserJWT);
            await _unitOfWork.SaveChangesAsync();

            return new AuthModel
            {
                JWTModel = new()
                {
                    JWT = newUserJWT.JWT,
                    JWTExpireDate = newUserJWT.JWTExpireDate
                },
                RefreshJWTModel = new()
                {
                    RefreshJWT = newUserJWT.RefreshJWT,
                    RefreshJWTExpireDate = newUserJWT.RefreshJWTExpireDate
                }
            };
        }


        /// <summary>
        /// Generates a two-factor authentication (2FA) verification code to be sent via email.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>The verification code as a string.</returns>
        public async Task<string> GenerateVerificationCodeAsync(User user)
        {
            var code = await _unitOfWork.Users.UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            return code;
        }


        /// <summary>
        /// Verifies whether the entered two-factor authentication (2FA) code is valid.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <param name="code">The verification code to be checked.</param>
        /// <returns>True if the code is valid; otherwise, False.</returns>
        public async Task<bool> VerifyCodeAsync(User user, string code)
        {
            var isValid = await _unitOfWork.Users.UserManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, code);
            return isValid;
        }


        /// <summary>
        /// Confirms the user's email address using the provided token.
        /// </summary>
        /// <param name="emailRequest">The request containing the user ID and confirmation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.  
        /// The task result contains an <see cref="EmailConfirmationResponse"/> indicating whether the email was confirmed successfully.
        /// </returns>
        public async Task<EmailConfirmationResponse> ConfirmEmailAsync(EmailConfirmationRequest emailRequest)
        {
            var emailResponse = new EmailConfirmationResponse
            {
                UserId = emailRequest.UserId,
            };

            var user = await _unitOfWork.Users.UserManager.FindByIdAsync(emailRequest.UserId);
            if (user == null)
            {
                emailResponse.IsConfirmed = false;
                emailResponse.Message = "User not found";
                return emailResponse;
            }

            if (user.EmailConfirmed)
            {
                emailResponse.IsConfirmed = true;
                emailResponse.Message = "Email has been confirmed in advance";
                return emailResponse;
            }

            var result = await _unitOfWork.Users.UserManager.ConfirmEmailAsync(user, emailRequest.Token);
            if (result.Succeeded)
            {
                emailResponse.IsConfirmed = true;
                emailResponse.Message = "Email confirmed successfully!";
                return emailResponse;
            }

            emailResponse.IsConfirmed = false;
            emailResponse.Message = "The Token is incorrect";
            return emailResponse;
        }
    }
}
