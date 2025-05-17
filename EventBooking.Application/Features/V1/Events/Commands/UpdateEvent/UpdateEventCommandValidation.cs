using FluentValidation;

namespace EventBooking.Application.Features.V1.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidation : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(e => e.UpdateEvent.Id)
                .NotNull().WithMessage(e => $"{nameof(e.UpdateEvent.Id)} can not be null.")
                .NotEmpty().WithMessage(e => $"{nameof(e.UpdateEvent.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(e => $"{nameof(e.UpdateEvent.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(e => $"{nameof(e.UpdateEvent.Id)} can not less than 36 Characters.");


            RuleFor(e => e.UpdateEvent.Name)
                 .NotEmpty().WithMessage(e => $"{nameof(e.UpdateEvent.Name)} is required.")
                 .MaximumLength(100).WithMessage(e => $"{nameof(e.UpdateEvent.Name)} cannot exceed 100 characters.");

            RuleFor(e => e.UpdateEvent.Description)
                .NotEmpty().WithMessage(e => $"{nameof(e.UpdateEvent.Description)} is required.")
                .MaximumLength(1000).WithMessage(e => $"{nameof(e.UpdateEvent.Description)} cannot exceed 1000 characters.");

            RuleFor(e => e.UpdateEvent.Category)
                .NotEmpty().WithMessage(e => $"{nameof(e.UpdateEvent.Category)}Category is required.");

            RuleFor(e => e.UpdateEvent.StartDate)
                .NotEmpty().WithMessage(e => $"{nameof(e.UpdateEvent.StartDate)} is required.")
                .Must(date => date > DateTime.Now).WithMessage(e => $"{nameof(e.UpdateEvent.StartDate)} must be in the future.");

            RuleFor(e => e.UpdateEvent.Venue)
                .NotEmpty().WithMessage(e => $"{nameof(e.UpdateEvent.Venue)} is required.")
                .MaximumLength(200).WithMessage(e => $"{nameof(e.UpdateEvent.Venue)} cannot exceed 200 characters.");

            RuleFor(e => e.UpdateEvent.Price)
                .GreaterThanOrEqualTo(0).WithMessage(e => $"{nameof(e.UpdateEvent.Price)} must be zero or positive.");

            RuleFor(e => e.UpdateEvent.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage("Image URL must be a valid URL.");
        }
    }
}
