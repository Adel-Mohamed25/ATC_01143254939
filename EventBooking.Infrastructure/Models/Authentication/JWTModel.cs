namespace EventBooking.Infrastructure.Models.Authentication
{
    public class JWTModel
    {
        public string JWT { get; set; }
        public DateTime JWTExpireDate { get; set; }
    }
}
