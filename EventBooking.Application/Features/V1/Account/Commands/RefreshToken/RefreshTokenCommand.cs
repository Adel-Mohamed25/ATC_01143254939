using EventBooking.Application.DTOs.Account;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Models.Authentication;
using MediatR;

namespace EventBooking.Application.Features.V1.Account.Commands.RefreshToken
{
    public record RefreshTokenCommand(RefreshJwtRequestDTO RefreshJwtRequest) : IRequest<Response<AuthModel>>;
}
