using EventBooking.Domain.IRepositories;
using FluentValidation;

namespace EventBooking.Application.Features.V1.Account.Commands.Register
{
    public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {
            RuleFor(r => r.RegisterDTO.FirstName)
                .NotNull().WithMessage(r => $"{nameof(r.RegisterDTO.FirstName)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.FirstName)} can not be null.")
                .MaximumLength(100).WithMessage(r => $"{nameof(r.RegisterDTO.FirstName)} can not exceed 100 characters.")
                .MinimumLength(3).WithMessage(r => $"{nameof(r.RegisterDTO.FirstName)} can not less than 3 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(r => $"{nameof(r.RegisterDTO.FirstName)} contains invalid characters.");

            RuleFor(r => r.RegisterDTO.LastName)
                .NotNull().WithMessage(r => $"{nameof(r.RegisterDTO.LastName)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.LastName)} can not be null.")
                .MaximumLength(100).WithMessage(r => $"{nameof(r.RegisterDTO.LastName)} can not exceed 100 characters.")
                .MinimumLength(3).WithMessage(r => $"{nameof(r.RegisterDTO.LastName)} can not less than 3 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(r => $"{nameof(r.RegisterDTO.LastName)} contains invalid characters.");

            RuleFor(r => r.RegisterDTO.Email)
                .NotNull().WithMessage(r => $"{nameof(r.RegisterDTO.Email)} is required.")
                .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.Email)} Can not be empty.")
                .EmailAddress().WithMessage(r => $"Invalid{nameof(r.RegisterDTO.Email)} format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage(r => $"Invalid {nameof(r.RegisterDTO.Email)} format. Please enter a valid email address.")
                .MustAsync(IsEmailUnique).WithMessage(r => $"{nameof(r.RegisterDTO.Email)} already exists.");

            RuleFor(r => r.RegisterDTO.Address)
                .NotNull().WithMessage(r => $"{nameof(r.RegisterDTO.Address)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.Address)} can not be empty.")
                .MinimumLength(3).WithMessage(r => $"{nameof(r.RegisterDTO.Address)} can not less than 5 characters.")
                .MaximumLength(200).WithMessage(r => $"{nameof(r.RegisterDTO.Address)} can not exceed 200 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(r => $"{nameof(r.RegisterDTO.Address)} contains invalid characters.");


            RuleFor(r => r.RegisterDTO.PhoneNumber)
              .NotNull().WithMessage(r => $"{nameof(r.RegisterDTO.PhoneNumber)} can not be null.")
              .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.PhoneNumber)} can not be empty.")
              .Matches("^(0|٠)1[0-9٠-٩]{9}$").WithMessage(r => $"{nameof(r.RegisterDTO.PhoneNumber)} must start with 01 and contain exactly 11 digits. Letters and special characters are not allowed.");


            RuleFor(r => r.RegisterDTO.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(r => $"{nameof(r.RegisterDTO.DateOfBirth)} cannot be in the future.");


            RuleFor(r => r.RegisterDTO.Gender)
              .NotNull().WithMessage(u => $"{nameof(u.RegisterDTO.Gender)} can not be null.")
              .IsInEnum().WithMessage(u => $"Invalid {nameof(u.RegisterDTO.Gender)} value. Allowed values are Male (0), Female (1).");


            RuleFor(r => r.RegisterDTO.Password)
                .NotNull().WithMessage(r => $"{nameof(r.RegisterDTO.Password)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.Password)} can not be empty.")
                .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.Password)} is required.")
                .MinimumLength(8).WithMessage(r => $"{nameof(r.RegisterDTO.Password)} must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage(r => $"{nameof(r.RegisterDTO.Password)} must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage(r => $"{nameof(r.RegisterDTO.Password)} must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage(r => $"{nameof(r.RegisterDTO.Password)} must contain at least one digit.")
                .Matches(@"[^a-zA-Z0-9]").WithMessage(r => $"{nameof(r.RegisterDTO.Password)} must contain at least one non-alphanumeric character.")
                .Must(HaveUniqueCharacters).WithMessage(r => $"{nameof(r.RegisterDTO.Password)} must have at least 8 unique characters.");

            RuleFor(rp => rp.RegisterDTO.ConfirmPassword)
                 .NotNull().WithMessage(r => $"{nameof(r.RegisterDTO.ConfirmPassword)} can not be null.")
                 .NotEmpty().WithMessage(r => $"{nameof(r.RegisterDTO.ConfirmPassword)} can not be empty")
                 .Equal(rp => rp.RegisterDTO.Password).WithMessage(r => $"{nameof(r.RegisterDTO.Password)} and {nameof(r.RegisterDTO.ConfirmPassword)} do not match.");

        }

        private bool HaveUniqueCharacters(string password)
        {
            return password.Distinct().Count() >= 8;
        }

        private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.Users.IsExistAsync(u => u.Email!.Equals(email), cancellationToken);
        }
    }
}
