using AutoMapper;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.Entities;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookingCommandHandler> _logger;

        public CreateBookingCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateBookingCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = _mapper.Map<Booking>(request.CreateBooking);
                await _unitOfWork.Bookings.CreateAsync(booking, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The booking has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create booking.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
