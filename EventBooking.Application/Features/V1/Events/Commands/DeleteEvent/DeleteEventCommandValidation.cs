using FluentValidation;

namespace EventBooking.Application.Features.V1.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandValidation : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(e => e.Id)
                .NotNull().WithMessage(e => $"{nameof(e.Id)} can not be null.")
                .NotEmpty().WithMessage(e => $"{nameof(e.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(e => $"{nameof(e.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(e => $"{nameof(e.Id)} can not less than 36 Characters.");
        }
    }
}
