using AutoMapper;
using EventBooking.Application.DTOs.Event;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Events.Queries.GetPaginatedEvents
{
    public class GetPaginatedEventsQueryHandler
        : IRequestHandler<GetPaginatedEventsQuery, ResponsePagination<IEnumerable<EventDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedEventsQueryHandler> _logger;

        public GetPaginatedEventsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetPaginatedEventsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ResponsePagination<IEnumerable<EventDTO>>> Handle(GetPaginatedEventsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var events = await _unitOfWork.Events.GetAllAsync(orderBy: e => e.Name,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!events.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<EventDTO>>(message: "Events not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.Events.CountAsync(cancellationToken: cancellationToken));

                var data = _mapper.Map<IEnumerable<EventDTO>>(events);
                return ResponsePaginationHandler.Success(data: data,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.Events.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving events data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<EventDTO>>(errors: ex.Message);
            }
        }
    }
}
