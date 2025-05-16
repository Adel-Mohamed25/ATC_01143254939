namespace EventBooking.Infrastructure.Models.Authentication
{
    public class RefreshJWTModel
    {
        public string RefreshJWT { get; set; }
        public DateTime RefreshJWTExpireDate { get; set; }
    }
}
