using AutoMapper;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileServices _services;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEventCommandHandler> _logger;

        public UpdateEventCommandHandler(IUnitOfWork unitOfWork,
            IFileServices services,
            IMapper mapper,
            ILogger<UpdateEventCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _services = services;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var eventMpdel = await _unitOfWork.Events.GetByAsync(e => e.Id.Equals(request.UpdateEvent.Id), cancellationToken: cancellationToken);
                if (eventMpdel is null)
                    return ResponseHandler.NotFound<string>(message: "Event not found.");

                if (request.UpdateEvent.Image is null && request.UpdateEvent.ImageUrl is null && eventMpdel.ImageUrl is not null)
                {
                    await _services.DeleteImageAsync("EventImages", eventMpdel.ImageUrl);
                    request.UpdateEvent.ImageUrl = null;
                }
                else if (request.UpdateEvent.Image is not null)
                {
                    if (!string.IsNullOrEmpty(eventMpdel.ImageUrl))
                    {
                        await _services.DeleteImageAsync("EventImages", eventMpdel.ImageUrl);
                    }
                    var imageUrl = await _services.UploadImageAsync("EventImages", request.UpdateEvent.Image);
                    request.UpdateEvent.ImageUrl = imageUrl;
                }

                _mapper.Map(request.UpdateEvent, eventMpdel);

                await _unitOfWork.Events.UpdateAsync(eventMpdel, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The Event has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update Event.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
