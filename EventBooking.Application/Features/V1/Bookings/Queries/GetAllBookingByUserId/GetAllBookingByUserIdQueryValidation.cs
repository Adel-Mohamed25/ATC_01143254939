using FluentValidation;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetAllBookingByUserId
{
    public class GetAllBookingByUserIdQueryValidation : AbstractValidator<GetAllBookingByUserIdQuery>
    {
        public GetAllBookingByUserIdQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(b => b.UserId)
                .NotNull().WithMessage(b => $"{nameof(b.UserId)} can not be null.")
                .NotEmpty().WithMessage(b => $"{nameof(b.UserId)} can not be empty.")
                .MaximumLength(36).WithMessage(b => $"{nameof(b.UserId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(b => $"{nameof(b.UserId)} can not less than 36 Characters.");

        }
    }
}
