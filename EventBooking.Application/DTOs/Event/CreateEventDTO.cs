using Microsoft.AspNetCore.Http;

namespace EventBooking.Application.DTOs.Event
{
    public class CreateEventDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime StartDate { get; set; }
        public string Venue { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
