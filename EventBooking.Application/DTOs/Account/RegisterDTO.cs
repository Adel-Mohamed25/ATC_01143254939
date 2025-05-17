using EventBooking.Domain.Enums;

namespace EventBooking.Application.DTOs.Account
{
    public class RegisterDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
