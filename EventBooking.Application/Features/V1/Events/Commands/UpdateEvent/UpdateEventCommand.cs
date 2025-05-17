using EventBooking.Application.DTOs.Event;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Events.Commands.UpdateEvent
{
    public record UpdateEventCommand(UpdateEventDTO UpdateEvent) : IRequest<Response<string>>;
}
