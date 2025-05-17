using EventBooking.Application.DTOs.Booking;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetAllBookingByUserId
{
    public record GetAllBookingByUserIdQuery(string UserId) : IRequest<Response<IEnumerable<BookingDTO>>>;
}
