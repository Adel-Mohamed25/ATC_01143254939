using Microsoft.AspNetCore.Identity;

namespace EventBooking.Domain.Entities.IdentityEntities
{
    public class Role : IdentityRole<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
