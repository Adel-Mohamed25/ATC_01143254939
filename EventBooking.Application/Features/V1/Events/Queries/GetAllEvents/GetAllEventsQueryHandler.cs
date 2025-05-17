using AutoMapper;
using EventBooking.Application.DTOs.Event;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.Enums;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Events.Queries.GetAllEvents
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, Response<IEnumerable<EventDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllEventsQueryHandler> _logger;

        public GetAllEventsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetAllEventsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<IEnumerable<EventDTO>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var events = await _unitOfWork.Events.GetAllAsync(orderBy: e => e.Name!,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!events.Any())
                    return ResponseHandler.NotFound<IEnumerable<EventDTO>>(message: "Events not found.");

                var result = _mapper.Map<IEnumerable<EventDTO>>(events);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Events data.");
                return ResponseHandler.BadRequest<IEnumerable<EventDTO>>(errors: ex.Message);
            }
        }
    }
}
