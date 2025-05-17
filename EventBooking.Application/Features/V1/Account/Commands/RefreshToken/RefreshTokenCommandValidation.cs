using FluentValidation;

namespace EventBooking.Application.Features.V1.Account.Commands.RefreshToken
{
    public class RefreshTokenCommandValidation : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(rt => rt.RefreshJwtRequest.Jwt)
                .NotNull().WithMessage("Jwt can not be null.")
                .NotEmpty().WithMessage("Jwt can be empty.");

            RuleFor(rt => rt.RefreshJwtRequest.RefreshJwt)
                .NotNull().WithMessage("RefreshJwt  can not be null.")
                .NotEmpty().WithMessage("JRefreshJwt can be empty.");
        }
    }
}
