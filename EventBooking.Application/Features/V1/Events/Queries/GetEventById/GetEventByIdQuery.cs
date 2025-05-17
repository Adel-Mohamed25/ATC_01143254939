using EventBooking.Application.DTOs.Event;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Events.Queries.GetEventById
{
    public record GetEventByIdQuery(string Id) : IRequest<Response<EventDTO>>;
}
