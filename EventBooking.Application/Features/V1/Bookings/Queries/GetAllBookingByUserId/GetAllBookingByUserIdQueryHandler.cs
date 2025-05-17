using AutoMapper;
using EventBooking.Application.DTOs.Booking;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.Enums;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetAllBookingByUserId
{
    public class GetAllBookingByUserIdQueryHandler
        : IRequestHandler<GetAllBookingByUserIdQuery, Response<IEnumerable<BookingDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllBookingByUserIdQueryHandler> _logger;

        public GetAllBookingByUserIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetAllBookingByUserIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<IEnumerable<BookingDTO>>> Handle(GetAllBookingByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookings = await _unitOfWork.Bookings.GetAllAsync(firstFilter: b => b.UserId.Equals(request.UserId),
                    orderBy: b => b.Id!,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!bookings.Any())
                    return ResponseHandler.NotFound<IEnumerable<BookingDTO>>(message: "User don't has bookings not found.");

                var result = _mapper.Map<IEnumerable<BookingDTO>>(bookings);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving bookings data for user.");
                return ResponseHandler.BadRequest<IEnumerable<BookingDTO>>(errors: ex.Message);
            }
        }
    }
}

