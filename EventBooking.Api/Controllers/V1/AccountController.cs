using EventBooking.Api.Controllers.Common;
using EventBooking.Application.DTOs.Account;
using EventBooking.Application.Features.V1.Account.Commands.ConfirmEmail;
using EventBooking.Application.Features.V1.Account.Commands.Login;
using EventBooking.Application.Features.V1.Account.Commands.RefreshToken;
using EventBooking.Application.Features.V1.Account.Commands.Register;
using EventBooking.Application.Features.V1.Account.Commands.ResetPassword;
using EventBooking.Infrastructure.Models.Email;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            return NewResult(await Mediator.Send(new RegisterCommand(registerDTO)));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            return NewResult(await Mediator.Send(new LoginCommand(loginDTO)));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshJwtRequestDTO refreshJwtRequestDTO)
        {
            return NewResult(await Mediator.Send(new RefreshTokenCommand(refreshJwtRequestDTO)));
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            return NewResult(await Mediator.Send(new ResetPasswordCommand(resetPasswordDTO)));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailConfirmationRequest emailConfirmation)
        {
            return await Mediator.Send(new ConfirmEmailCommand(emailConfirmation));
        }

    }
}
