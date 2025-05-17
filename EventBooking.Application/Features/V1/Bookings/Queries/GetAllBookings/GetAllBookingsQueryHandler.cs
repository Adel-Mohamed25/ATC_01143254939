using AutoMapper;
using EventBooking.Application.DTOs.Booking;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.Enums;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetAllBookings
{
    public class GetAllBookingsQueryHandler
        : IRequestHandler<GetAllBookingsQuery, Response<IEnumerable<BookingDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllBookingsQueryHandler> _logger;

        public GetAllBookingsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetAllBookingsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<IEnumerable<BookingDTO>>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookings = await _unitOfWork.Bookings.GetAllAsync(orderBy: b => b.Id!,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!bookings.Any())
                    return ResponseHandler.NotFound<IEnumerable<BookingDTO>>(message: "Bookings not found.");

                var result = _mapper.Map<IEnumerable<BookingDTO>>(bookings);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving bookings data.");
                return ResponseHandler.BadRequest<IEnumerable<BookingDTO>>(errors: ex.Message);
            }
        }
    }
}
