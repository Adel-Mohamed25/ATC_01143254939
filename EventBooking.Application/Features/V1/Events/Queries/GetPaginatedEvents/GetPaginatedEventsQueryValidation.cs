using FluentValidation;

namespace EventBooking.Application.Features.V1.Events.Queries.GetPaginatedEvents
{
    public class GetPaginatedEventsQueryValidation : AbstractValidator<GetPaginatedEventsQuery>
    {
        public GetPaginatedEventsQueryValidation()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(gp => gp.Pagination.PageNumber)
                .NotNull().WithMessage(gp => $"{nameof(gp.Pagination.PageNumber)} can not be null.")
                .NotEmpty().WithMessage(gp => $"{nameof(gp.Pagination.PageNumber)} can not be empty.")
                .GreaterThan(0).WithMessage(gp => $"{nameof(gp.Pagination.PageNumber)} can not be less than or equal 0.");

            RuleFor(gp => gp.Pagination.PageSize)
                .NotNull().WithMessage(gp => $"{nameof(gp.Pagination.PageSize)} can not be null.")
                .NotEmpty().WithMessage(gp => $"{nameof(gp.Pagination.PageSize)} can not be empty.")
                .GreaterThan(0).WithMessage(gp => $"{nameof(gp.Pagination.PageSize)} can not be less than or equal 0.")
                .LessThanOrEqualTo(50).WithMessage("PageSize must not exceed 50.");


            RuleFor(gp => gp.Pagination.OrderByDirection)
                .NotNull().WithMessage(gp => $"{nameof(gp.Pagination.OrderByDirection)} can not be null.")
                .NotEmpty().WithMessage(gp => $"{nameof(gp.Pagination.OrderByDirection)} can not be empty.")
                .IsInEnum().WithMessage(gp => $"Invalid {nameof(gp.Pagination.OrderByDirection)} value. Allowed values are Ascending (1), Descending (2).");

        }
    }
}
