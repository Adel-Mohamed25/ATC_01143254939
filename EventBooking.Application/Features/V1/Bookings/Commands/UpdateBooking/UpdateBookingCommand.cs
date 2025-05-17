using EventBooking.Application.DTOs.Booking;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Bookings.Commands.UpdateBooking
{
    public record UpdateBookingCommand(UpdateBookingDTO UpdateBooking) : IRequest<Response<string>>;
}
