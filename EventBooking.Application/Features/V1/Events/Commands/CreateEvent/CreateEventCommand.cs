using EventBooking.Application.DTOs.Event;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Events.Commands.CreateEvent
{
    public record CreateEventCommand(CreateEventDTO CreateEvent) : IRequest<Response<string>>;
}
