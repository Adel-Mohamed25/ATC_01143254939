namespace EventBooking.Application.DTOs.Booking
{
    public class BookingDTO
    {
        public string Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string UserId { get; set; }
        public string EventId { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
