using FluentValidation;

namespace EventBooking.Application.Features.V1.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidation : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(e => e.CreateEvent.Name)
                 .NotEmpty().WithMessage(e => $"{nameof(e.CreateEvent.Name)} is required.")
                 .MaximumLength(100).WithMessage(e => $"{nameof(e.CreateEvent.Name)} cannot exceed 100 characters.");

            RuleFor(e => e.CreateEvent.Description)
                .NotEmpty().WithMessage(e => $"{nameof(e.CreateEvent.Description)} is required.")
                .MaximumLength(1000).WithMessage(e => $"{nameof(e.CreateEvent.Description)} cannot exceed 1000 characters.");

            RuleFor(e => e.CreateEvent.Category)
                .NotEmpty().WithMessage(e => $"{nameof(e.CreateEvent.Category)}Category is required.");

            RuleFor(e => e.CreateEvent.StartDate)
                .NotEmpty().WithMessage(e => $"{nameof(e.CreateEvent.StartDate)} is required.")
                .Must(date => date > DateTime.Now).WithMessage(e => $"{nameof(e.CreateEvent.StartDate)} must be in the future.");

            RuleFor(e => e.CreateEvent.Venue)
                .NotEmpty().WithMessage(e => $"{nameof(e.CreateEvent.Venue)} is required.")
                .MaximumLength(200).WithMessage(e => $"{nameof(e.CreateEvent.Venue)} cannot exceed 200 characters.");

            RuleFor(e => e.CreateEvent.Price)
                .GreaterThanOrEqualTo(0).WithMessage(e => $"{nameof(e.CreateEvent.Price)} must be zero or positive.");

        }
    }
}
