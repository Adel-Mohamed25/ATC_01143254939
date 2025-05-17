using EventBooking.Api.Controllers.Common;
using EventBooking.Application.DTOs.Booking;
using EventBooking.Application.DTOs.Pagination;
using EventBooking.Application.Features.V1.Bookings.Commands.CreateBooking;
using EventBooking.Application.Features.V1.Bookings.Commands.DeleteBooking;
using EventBooking.Application.Features.V1.Bookings.Commands.UpdateBooking;
using EventBooking.Application.Features.V1.Bookings.Queries.GetAllBookingByUserId;
using EventBooking.Application.Features.V1.Bookings.Queries.GetAllBookings;
using EventBooking.Application.Features.V1.Bookings.Queries.GetBookingById;
using EventBooking.Application.Features.V1.Bookings.Queries.GetPaginatedBookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BookingController : BaseApiController
    {
        [Authorize(Roles = "Admin, User")]
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO createBooking)
        {
            return NewResult(await Mediator.Send(new CreateBookingCommand(createBooking)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateBooking")]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingDTO updateBooking)
        {
            return NewResult(await Mediator.Send(new UpdateBookingCommand(updateBooking)));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteBooking")]
        public async Task<IActionResult> DeleteBooking([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new DeleteBookingCommand(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            return NewResult(await Mediator.Send(new GetAllBookingsQuery()));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetPaginatedBookings")]
        public async Task<IActionResult> GetPaginatedBookings([FromQuery] PaginationDTO pagination)
        {
            return NewResult(await Mediator.Send(new GetPaginatedBookingsQuery(pagination)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetBookingById")]
        public async Task<IActionResult> GetBookingById([FromQuery] string id)
        {
            return NewResult(await Mediator.Send(new GetBookingByIdQuery(id)));
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetAllBookingByUserId")]
        public async Task<IActionResult> GetAllBookingByUserId([FromQuery] string userId)
        {
            return NewResult(await Mediator.Send(new GetAllBookingByUserIdQuery(userId)));
        }
    }
}
