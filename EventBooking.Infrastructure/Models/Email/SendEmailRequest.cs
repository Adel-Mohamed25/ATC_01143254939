using Microsoft.AspNetCore.Http;

namespace EventBooking.Infrastructure.Models.Email
{
    public class SendEmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile>? Attachments { get; set; }
    }
}
