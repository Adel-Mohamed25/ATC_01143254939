using EventBooking.Infrastructure.Models.Email;

namespace EventBooking.Infrastructure.Services.Abstractions
{
    public interface IEmailServices
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailRequest sendEmailModel);
    }
}
