using EventBooking.Application.DTOs.Account;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Account.Commands.Register
{
    public record RegisterCommand(RegisterDTO RegisterDTO) : IRequest<Response<string>>;
}
