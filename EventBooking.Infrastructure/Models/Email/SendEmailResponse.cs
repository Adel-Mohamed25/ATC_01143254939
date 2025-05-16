namespace EventBooking.Infrastructure.Models.Email
{
    public class SendEmailResponse
    {
        public string To { get; set; }
        public string From { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
