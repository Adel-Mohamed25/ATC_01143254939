using EventBooking.Application.DTOs.Account;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Account.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordDTO ResetPasswordDTO) : IRequest<Response<string>>;

}
