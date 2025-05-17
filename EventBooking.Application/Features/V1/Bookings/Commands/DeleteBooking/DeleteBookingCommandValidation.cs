using FluentValidation;

namespace EventBooking.Application.Features.V1.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandValidation : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(b => b.Id)
                .NotNull().WithMessage(b => $"{nameof(b.Id)} can not be null.")
                .NotEmpty().WithMessage(b => $"{nameof(b.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(b => $"{nameof(b.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(b => $"{nameof(b.Id)} can not less than 36 Characters.");

        }
    }
}
