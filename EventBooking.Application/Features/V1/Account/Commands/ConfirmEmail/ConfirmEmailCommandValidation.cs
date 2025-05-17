using FluentValidation;

namespace EventBooking.Application.Features.V1.Account.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidation : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(cm => cm.EmailConfirmation.UserId)
                .NotNull().WithMessage("UserId is required.")
                .NotEmpty().WithMessage("UserId can not be empty.")
                .MaximumLength(36).WithMessage("UserId max length 36 characters")
                .MinimumLength(36).WithMessage("UserId mini length 36 characters");

            RuleFor(cm => cm.EmailConfirmation.Token)
                .NotNull().WithMessage("Token is required.")
                .NotEmpty().WithMessage("Token can not be empty.");
        }
    }
}
