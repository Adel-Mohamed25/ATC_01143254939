using EventBooking.Application.DTOs.Booking;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetAllBookings
{
    public record GetAllBookingsQuery() : IRequest<Response<IEnumerable<BookingDTO>>>;
}
