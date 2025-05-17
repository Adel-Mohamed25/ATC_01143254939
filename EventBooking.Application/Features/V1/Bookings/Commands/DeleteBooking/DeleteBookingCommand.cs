using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Bookings.Commands.DeleteBooking
{
    public record DeleteBookingCommand(string Id) : IRequest<Response<string>>;
}
