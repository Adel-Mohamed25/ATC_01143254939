using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Account.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork,
            ILogger<ResetPasswordCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.Users.UserManager.FindByEmailAsync(request.ResetPasswordDTO.Email);
                if (user is null)
                    return ResponseHandler.NotFound<string>(message: "User Not Found");

                var token = await _unitOfWork.Users.UserManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await _unitOfWork.Users.UserManager.ResetPasswordAsync(user, token, request.ResetPasswordDTO.Password);
                if (!result.Succeeded)
                    return ResponseHandler.Conflict<string>(errors: "Password change failed.");
                return ResponseHandler.Success<string>(message: "Password changed successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during user reset password.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
