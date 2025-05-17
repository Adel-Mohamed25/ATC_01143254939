using EventBooking.Application.DTOs.Event;
using EventBooking.Application.DTOs.Pagination;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Events.Queries.GetPaginatedEvents
{
    public record GetPaginatedEventsQuery(PaginationDTO Pagination)
        : IRequest<ResponsePagination<IEnumerable<EventDTO>>>;
}
