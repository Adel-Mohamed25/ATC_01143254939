using AutoMapper;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.Entities;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileServices _services;
        private readonly ILogger<CreateEventCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IUnitOfWork unitOfWork,
            IFileServices services,
            ILogger<CreateEventCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _services = services;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var eventModel = _mapper.Map<Event>(request.CreateEvent);
                if (request.CreateEvent.Image is not null)
                {
                    var imageUrl = await _services.UploadImageAsync("EventImages", request.CreateEvent.Image);
                    eventModel.ImageUrl = imageUrl;
                }
                await _unitOfWork.Events.CreateAsync(eventModel, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The Event has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create Event.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
