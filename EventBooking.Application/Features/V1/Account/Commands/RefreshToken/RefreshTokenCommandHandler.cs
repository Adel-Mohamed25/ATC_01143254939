using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Models.Authentication;
using EventBooking.Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Account.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<AuthModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthServices _services;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork,
            IAuthServices services,
            ILogger<RefreshTokenCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _services = services;
            _logger = logger;
        }
        public async Task<Response<AuthModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var jwtToken = await _unitOfWork.JwtTokens.GetByAsync(jt =>
                           jt.JWT == request.RefreshJwtRequest.Jwt
                           && jt.RefreshJWT == request.RefreshJwtRequest.RefreshJwt
                           && jt.IsRefreshJWTUsed
                           , includes: "User");

                if (jwtToken == null)
                    return ResponseHandler.NotFound<AuthModel>(message: "Token and RefreshToken Not Found");

                var jwtSecurityToken = await _services.ReadTokenAsync(request.RefreshJwtRequest.Jwt);
                if (jwtSecurityToken == null)
                    return ResponseHandler.Unauthorized<AuthModel>(message: "Invalid JWT Token");

                bool isValid = await _services.IsTokenValidAsync(request.RefreshJwtRequest.Jwt, jwtSecurityToken);

                if (!isValid)
                    return ResponseHandler.Unauthorized<AuthModel>(message: "Token and RefreshToken Not valid");

                var refreshToken = await _services.GetRefreshTokenAsync(jwtToken.User);
                return ResponseHandler.Success(refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during user refresh token.");
                return ResponseHandler.BadRequest<AuthModel>(errors: ex.Message);
            }
        }
    }
}
