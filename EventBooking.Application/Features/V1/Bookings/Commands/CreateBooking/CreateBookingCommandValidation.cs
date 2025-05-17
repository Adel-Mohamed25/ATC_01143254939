using EventBooking.Domain.IRepositories;
using FluentValidation;

namespace EventBooking.Application.Features.V1.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidation : AbstractValidator<CreateBookingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookingCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {
            RuleFor(b => b.CreateBooking.UserId)
               .NotNull().WithMessage(b => $"{nameof(b.CreateBooking.UserId)} can not be null.")
               .NotEmpty().WithMessage(b => $"{nameof(b.CreateBooking.UserId)} can not be empty.")
               .MaximumLength(36).WithMessage(b => $"{nameof(b.CreateBooking.UserId)} can not exceed 36 Characters.")
               .MinimumLength(36).WithMessage(b => $"{nameof(b.CreateBooking.UserId)} can not less than 36 Characters.");

            RuleFor(b => b.CreateBooking.EventId)
                .NotNull().WithMessage(b => $"{nameof(b.CreateBooking.EventId)} can not be null.")
                .NotEmpty().WithMessage(b => $"{nameof(b.CreateBooking.EventId)} can not be empty.")
                .MaximumLength(36).WithMessage(b => $"{nameof(b.CreateBooking.EventId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(b => $"{nameof(b.CreateBooking.EventId)} can not less than 36 Characters.");

            RuleFor(b => b.CreateBooking)
                .MustAsync((booking, cancellation) => IsBookingUnique(booking.UserId,
                booking.EventId, cancellation))
                .WithMessage("This user has already booked this event.");

        }

        private async Task<bool> IsBookingUnique(string userId, string eventId, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.Bookings.IsExistAsync(b => b.UserId == userId
            && b.EventId == eventId,
               cancellationToken
            );
        }
    }
}
