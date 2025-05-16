using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventBooking.Api.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();


        public ObjectResult NewResult<TData>(Response<TData> response) where TData : class
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.Conflict => new ConflictObjectResult(response),
                _ => new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                }
            };
        }
    }
}
