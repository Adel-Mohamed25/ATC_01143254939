using AutoMapper;
using EventBooking.Application.DTOs.Booking;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetPaginatedBookings
{
    public class GetPaginatedBookingsQueryHandler
        : IRequestHandler<GetPaginatedBookingsQuery, ResponsePagination<IEnumerable<BookingDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedBookingsQueryHandler> _logger;

        public GetPaginatedBookingsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetPaginatedBookingsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ResponsePagination<IEnumerable<BookingDTO>>> Handle(GetPaginatedBookingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookings = await _unitOfWork.Bookings.GetAllAsync(orderBy: b => b.Id,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!bookings.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<BookingDTO>>(message: "Bookings not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.Bookings.CountAsync(cancellationToken: cancellationToken));

                var data = _mapper.Map<IEnumerable<BookingDTO>>(bookings);
                return ResponsePaginationHandler.Success(data: data,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.Bookings.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving bookings data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<BookingDTO>>(errors: ex.Message);
            }
        }
    }
}
