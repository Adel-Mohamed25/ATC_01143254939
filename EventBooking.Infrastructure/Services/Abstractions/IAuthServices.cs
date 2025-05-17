using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Infrastructure.Models.Authentication;
using EventBooking.Infrastructure.Models.Email;
using System.IdentityModel.Tokens.Jwt;

namespace EventBooking.Infrastructure.Services.Abstractions
{
    public interface IAuthServices
    {
        Func<string, JwtSecurityToken, Task<bool>> IsTokenValidAsync { get; }

        Task<AuthModel> GetTokenAsync(User user);

        Task<JwtSecurityToken> ReadTokenAsync(string jwt);

        Task<AuthModel> GetRefreshTokenAsync(User user);


        Task<EmailConfirmationResponse> ConfirmEmailAsync(EmailConfirmationRequest emailRequest);
    }
}
