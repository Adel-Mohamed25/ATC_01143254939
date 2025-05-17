using EventBooking.Application.DTOs.Account;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Models.Authentication;
using MediatR;

namespace EventBooking.Application.Features.V1.Account.Commands.Login
{
    public record LoginCommand(LoginDTO LoginDTO) : IRequest<Response<AuthModel>>;
}
