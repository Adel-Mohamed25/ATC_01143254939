using EventBooking.Api.Controllers.Common;
using EventBooking.Application.Features.V1.Email.Commands;
using EventBooking.Infrastructure.Models.Email;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmailController : BaseApiController
    {
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailRequest sendEmail)
        {
            return NewResult(await Mediator.Send(new SendEmailCommand(sendEmail)));
        }
    }
}
