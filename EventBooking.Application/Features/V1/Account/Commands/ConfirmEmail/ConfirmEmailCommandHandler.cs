using EventBooking.Application.Helper.ConfirmEmailMessage;
using EventBooking.Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Account.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, IActionResult>
    {
        private readonly IAuthServices _services;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;

        public ConfirmEmailCommandHandler(IAuthServices services,
            ILogger<ConfirmEmailCommandHandler> logger)
        {
            _services = services;
            _logger = logger;
        }
        public async Task<IActionResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _services.ConfirmEmailAsync(request.EmailConfirmation);

                if (response.IsConfirmed)
                {
                    if (response.Message == "Email has been confirmed in advance")
                    {
                        string htmlContent = ConfirmEmailFormat.GenerateHtmlContent("ℹ️ البريد الإلكتروني مؤكد مسبقًا. يمكنك تسجيل الدخول.",
                            "blue"
                        );

                        return new ContentResult
                        {
                            Content = htmlContent,
                            ContentType = "text/html",
                            StatusCode = 200
                        };
                    }

                    string successHtml = ConfirmEmailFormat.GenerateHtmlContent(
                        "✅ تم تأكيد البريد الإلكتروني بنجاح!",
                        "#2ecc71"
                    );
                    return new ContentResult
                    {
                        Content = successHtml,
                        ContentType = "text/html",
                        StatusCode = 200
                    };

                }

                string failureHtml = ConfirmEmailFormat.GenerateHtmlContent("❌ فشل تأكيد البريد الإلكتروني. الرجاء المحاولة لاحقًا.", "red");
                return new ContentResult
                {
                    Content = failureHtml,
                    ContentType = "text/html",
                    StatusCode = 400
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during confirm email.");
                return new ContentResult
                {
                    Content = $"<h2 style='text-align:center;color:red;'>❌ خطأ: {ex.Message}</h2>",
                    ContentType = "text/html",
                    StatusCode = 400
                };
            }

        }
    }
}
