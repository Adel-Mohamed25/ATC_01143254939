using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Models.Email;
using EventBooking.Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Email.Commands
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Response<SendEmailResponse>>
    {
        private readonly ILogger<SendEmailCommandHandler> _logger;
        private readonly IEmailServices _services;

        public SendEmailCommandHandler(IEmailServices services,
            ILogger<SendEmailCommandHandler> logger)
        {
            _logger = logger;
            _services = services;
        }
        public async Task<Response<SendEmailResponse>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _services.SendEmailAsync(request.SendEmail);
                if (response.IsSuccess)
                    return ResponseHandler.Success(response);
                return ResponseHandler.Conflict(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during send email");
                return ResponseHandler.Conflict<SendEmailResponse>(errors: ex.Message);
            }
        }
    }
}
