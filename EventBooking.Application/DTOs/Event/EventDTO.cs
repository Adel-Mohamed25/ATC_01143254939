namespace EventBooking.Application.DTOs.Event
{
    public class EventDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime StartDate { get; set; }
        public string Venue { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
