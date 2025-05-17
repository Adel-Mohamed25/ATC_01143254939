using EventBooking.Infrastructure.Models.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Application.Features.V1.Account.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(EmailConfirmationRequest EmailConfirmation) : IRequest<IActionResult>;
}
