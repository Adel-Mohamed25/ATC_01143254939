using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Events.Commands.DeleteEvent
{
    public record DeleteEventCommand(string Id) : IRequest<Response<string>>;

}
