using AutoMapper;
using EventBooking.Application.DTOs.Booking;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetBookingById
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, Response<BookingDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBookingByIdQueryHandler> _logger;

        public GetBookingByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetBookingByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<BookingDTO>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = await _unitOfWork.Bookings.GetByAsync(b => b.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (booking is null)
                    return ResponseHandler.NotFound<BookingDTO>(message: "Booking not found.");

                var result = _mapper.Map<BookingDTO>(booking);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving booking data.");
                return ResponseHandler.BadRequest<BookingDTO>(errors: ex.Message);
            }
        }
    }
}
