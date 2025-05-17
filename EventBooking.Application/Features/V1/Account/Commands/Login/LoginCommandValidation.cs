using FluentValidation;

namespace EventBooking.Application.Features.V1.Account.Commands.Login
{
    public class LoginCommandValidation : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(l => l.LoginDTO.Email)
                .NotNull().WithMessage("Email can not be null.")
                .NotEmpty().WithMessage("Email can not be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format. Please enter a valid email address.");

            RuleFor(l => l.LoginDTO.Password)
                .NotNull().WithMessage("Password can not be null.")
                .NotEmpty().WithMessage("Password can not be empty.");
        }
    }
}
