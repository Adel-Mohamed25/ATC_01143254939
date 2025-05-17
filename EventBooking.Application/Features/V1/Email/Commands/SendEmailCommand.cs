using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Models.Email;
using MediatR;

namespace EventBooking.Application.Features.V1.Email.Commands
{
    public record SendEmailCommand(SendEmailRequest SendEmail) : IRequest<Response<SendEmailResponse>>;
}
