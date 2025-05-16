using EventBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Domain.Entities.IdentityEntities
{
    public class User : IdentityUser<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<JwtToken> JwtTokens { get; set; } = new List<JwtToken>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
