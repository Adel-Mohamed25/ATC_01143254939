using FluentValidation;

namespace EventBooking.Application.Features.V1.Account.Commands.ResetPassword
{
    public class ResetPasswordCommandValidation : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(rp => rp.ResetPasswordDTO.Email)
                 .NotNull().WithMessage("Email is required.")
                 .NotEmpty().WithMessage("Email Can not be empty.")
                 .EmailAddress().WithMessage("Invalid email format.")
                 .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                 .WithMessage("Invalid email format. Please enter a valid email address.");

            RuleFor(rp => rp.ResetPasswordDTO.Password)
                .NotNull().WithMessage("Password can not be null.")
                .NotEmpty().WithMessage("Password can not be empty.")
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one digit.")
                .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.")
                .Must(HaveUniqueCharacters).WithMessage("Password must have at least 8 unique characters.");

            RuleFor(rp => rp.ResetPasswordDTO.ConfirmPassword)
                .NotNull().WithMessage("Confirm password can not be null.")
                .NotEmpty().WithMessage("Confirm password can not be empty")
                .Equal(rp => rp.ResetPasswordDTO.Password).WithMessage("Password and confirm password do not match.");
        }

        private bool HaveUniqueCharacters(string password)
        {
            return password.Distinct().Count() >= 8;
        }

    }
}
