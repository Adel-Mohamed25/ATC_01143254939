using EventBooking.Application.DTOs.Booking;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetBookingById
{
    public record GetBookingByIdQuery(string Id) : IRequest<Response<BookingDTO>>;
}
