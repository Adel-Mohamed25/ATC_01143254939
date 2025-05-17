using AutoMapper;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBookingCommandHandler> _logger;

        public UpdateBookingCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateBookingCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = await _unitOfWork.Bookings.GetByAsync(b => b.Id.Equals(request.UpdateBooking.Id),
                    cancellationToken: cancellationToken);

                if (booking is null)
                    return ResponseHandler.NotFound<string>(message: "Booking application not found.");

                _mapper.Map(request.UpdateBooking, booking);
                await _unitOfWork.Bookings.UpdateAsync(booking, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The booking has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update booking.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
