using EventBooking.Api.Controllers.Common;
using EventBooking.Application.DTOs.Event;
using EventBooking.Application.DTOs.Pagination;
using EventBooking.Application.Features.V1.Events.Commands.CreateEvent;
using EventBooking.Application.Features.V1.Events.Commands.DeleteEvent;
using EventBooking.Application.Features.V1.Events.Commands.UpdateEvent;
using EventBooking.Application.Features.V1.Events.Queries.GetAllEvents;
using EventBooking.Application.Features.V1.Events.Queries.GetEventById;
using EventBooking.Application.Features.V1.Events.Queries.GetPaginatedEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EventController : BaseApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromForm] CreateEventDTO createEventDTO)
        {
            return NewResult(await Mediator.Send(new CreateEventCommand(createEventDTO)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent([FromForm] UpdateEventDTO updateEventDTO)
        {
            return NewResult(await Mediator.Send(new UpdateEventCommand(updateEventDTO)));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteEventCommand(id)));
        }


        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            return NewResult(await Mediator.Send(new GetAllEventsQuery()));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetPaginatedEvents")]
        public async Task<IActionResult> GetPaginatedEvents([FromQuery] PaginationDTO pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedEventsQuery(pagination)));
        }


        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetEventById")]
        public async Task<IActionResult> GetEventById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetEventByIdQuery(id)));
        }
    }
}
