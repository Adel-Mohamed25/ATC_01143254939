namespace EventBooking.Infrastructure.Models.Email
{
    public class EmailConfirmationRequest
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
