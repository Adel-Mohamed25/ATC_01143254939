using EventBooking.Domain.Enums;

namespace EventBooking.Application.DTOs.Pagination
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public OrderByDirection OrderByDirection { get; set; }
    }
}
