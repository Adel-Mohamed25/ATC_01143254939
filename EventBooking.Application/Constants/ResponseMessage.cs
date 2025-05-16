namespace EventBooking.Application.Constants
{
    public sealed class ResponseMessage
    {
        public const string SuccessMessage = "The request has been completed successfully.";
        public const string NotFoundMessage = "The requested resource was not found.";
        public const string BadRequestMessage = "The request is invalid. Please check your input.";
        public const string UnAuthorizedMessage = "Unauthorized access. Please provide valid credentials.";
        public const string InternalServerErrorMessage = "An unexpected error occurred. Please try again later.";
        public const string ConflictErrorMessage = "The request could not be completed due to a conflict.";
        public const string ForbiddenMessage = "Access to the resource is forbidden.";
        public const string CreatedMessage = "The resource was created successfully.";
        public const string NoContentMessage = "The request was successful, but there is no content to return.";
        public const string ValidationErrorMessage = "One or more validation errors occurred.";
    }
}
