using EventBooking.Application.DTOs.Booking;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Bookings.Commands.CreateBooking
{
    public record CreateBookingCommand(CreateBookingDTO CreateBooking) : IRequest<Response<string>>;
}
