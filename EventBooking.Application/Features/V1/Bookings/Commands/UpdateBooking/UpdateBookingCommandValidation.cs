using FluentValidation;

namespace EventBooking.Application.Features.V1.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandValidation : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(b => b.UpdateBooking.Id)
                .NotNull().WithMessage(b => $"{nameof(b.UpdateBooking.Id)} can not be null.")
                .NotEmpty().WithMessage(b => $"{nameof(b.UpdateBooking.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(b => $"{nameof(b.UpdateBooking.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(b => $"{nameof(b.UpdateBooking.Id)} can not less than 36 Characters.");

            RuleFor(b => b.UpdateBooking.UserId)
               .NotNull().WithMessage(b => $"{nameof(b.UpdateBooking.UserId)} can not be null.")
               .NotEmpty().WithMessage(b => $"{nameof(b.UpdateBooking.UserId)} can not be empty.")
               .MaximumLength(36).WithMessage(b => $"{nameof(b.UpdateBooking.UserId)} can not exceed 36 Characters.")
               .MinimumLength(36).WithMessage(b => $"{nameof(b.UpdateBooking.UserId)} can not less than 36 Characters.");

            RuleFor(b => b.UpdateBooking.EventId)
                .NotNull().WithMessage(b => $"{nameof(b.UpdateBooking.EventId)} can not be null.")
                .NotEmpty().WithMessage(b => $"{nameof(b.UpdateBooking.EventId)} can not be empty.")
                .MaximumLength(36).WithMessage(b => $"{nameof(b.UpdateBooking.EventId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(b => $"{nameof(b.UpdateBooking.EventId)} can not less than 36 Characters.");

        }
    }
}
