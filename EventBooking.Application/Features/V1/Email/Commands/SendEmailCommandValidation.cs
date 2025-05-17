using FluentValidation;

namespace EventBooking.Application.Features.V1.Email.Commands
{
    public class SendEmailCommandValidation : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(e => e.SendEmail.To)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can not be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format. Please enter a valid email address.");


            RuleFor(e => e.SendEmail.Subject)
                .NotNull().WithMessage("Subject is required.")
                .NotEmpty().WithMessage("Subject can not be empty.")
                .MaximumLength(200).WithMessage("Subject can not exceed 200 characters.");

            RuleFor(e => e.SendEmail.Attachments)
                .Must(files => files == null || files.Count <= 5)
                .WithMessage("You can only attach up to 5 files.")
                .Must(files => files == null || files.All(file => file.Length <= 2 * 1024 * 1024))
                .WithMessage("Each file must be 2MB or smaller.");
        }
    }
}
