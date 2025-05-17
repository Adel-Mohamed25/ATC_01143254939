using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteEventCommandHandler> _logger;
        private readonly IFileServices _services;

        public DeleteEventCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteEventCommandHandler> logger,
            IFileServices services)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _services = services;
        }
        public async Task<Response<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var eventModel = await _unitOfWork.Events.GetByAsync(e => e.Id.Equals(request.Id), cancellationToken: cancellationToken);
                if (eventModel is null)
                    return ResponseHandler.NotFound<string>(message: "Event not found.");

                if (eventModel.ImageUrl is not null)
                    await _services.DeleteImageAsync("EventImages", eventModel.ImageUrl);

                await _unitOfWork.Events.DeleteAsync(eventModel, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The Event has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete Event.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
