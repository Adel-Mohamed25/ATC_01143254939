using EventBooking.Application.DTOs.Event;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Events.Queries.GetAllEvents
{
    public record GetAllEventsQuery() : IRequest<Response<IEnumerable<EventDTO>>>;
}
