using AutoMapper;
using EventBooking.Application.DTOs.Event;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Events.Queries.GetEventById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Response<EventDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEventByIdQueryHandler> _logger;

        public GetEventByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetEventByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<EventDTO>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var eventModel = await _unitOfWork.Events.GetByAsync(e => e.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (eventModel is null)
                    return ResponseHandler.NotFound<EventDTO>(message: "Event not found.");

                var result = _mapper.Map<EventDTO>(eventModel);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Event data.");
                return ResponseHandler.BadRequest<EventDTO>(errors: ex.Message);
            }
        }
    }
}
