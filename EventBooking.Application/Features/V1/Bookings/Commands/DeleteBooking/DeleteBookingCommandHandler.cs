using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteBookingCommandHandler> _logger;

        public DeleteBookingCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteBookingCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = await _unitOfWork.Bookings.GetByAsync(b => b.Id.Equals(request.Id), cancellationToken: cancellationToken);
                if (booking is null)
                    return ResponseHandler.NotFound<string>(message: "Booking not found.");

                await _unitOfWork.Bookings.DeleteAsync(booking, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The booking has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete booking.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
