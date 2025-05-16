using EventBooking.Domain.Commons;

namespace EventBooking.Domain.Entities.IdentityEntities
{
    public class JwtToken : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string JWT { get; set; }
        public string RefreshJWT { get; set; }
        public DateTime JWTExpireDate { get; set; }
        public DateTime RefreshJWTExpireDate { get; set; }
        public DateTime? RefreshJWTRevokedDate { get; set; }
        public bool IsRefreshJWTUsed { get; set; }
        public bool IsRefreshJWTExpired => DateTime.UtcNow >= RefreshJWTExpireDate;
        public bool IsRefreshJWTActive => !IsRefreshJWTExpired && RefreshJWTRevokedDate is null;

        public virtual User User { get; set; }
    }
}
