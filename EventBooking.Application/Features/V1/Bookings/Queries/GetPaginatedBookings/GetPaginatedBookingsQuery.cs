using EventBooking.Application.DTOs.Booking;
using EventBooking.Application.DTOs.Pagination;
using EventBooking.Domain.Utalities.ResponseModel;
using MediatR;

namespace EventBooking.Application.Features.V1.Bookings.Queries.GetPaginatedBookings
{
    public record GetPaginatedBookingsQuery(PaginationDTO Pagination)
        : IRequest<ResponsePagination<IEnumerable<BookingDTO>>>;

}
