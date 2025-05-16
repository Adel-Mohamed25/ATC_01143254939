using EventBooking.Domain.Commons;
using EventBooking.Domain.Entities.IdentityEntities;

namespace EventBooking.Domain.Entities
{
    public class Booking : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime BookingDate { get; set; }
        public string UserId { get; set; }
        public string EventId { get; set; }
        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
    }
}
