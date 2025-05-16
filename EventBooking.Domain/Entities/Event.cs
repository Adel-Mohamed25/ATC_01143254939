using EventBooking.Domain.Commons;

namespace EventBooking.Domain.Entities
{
    public class Event : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime StartDate { get; set; }
        public string Venue { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
