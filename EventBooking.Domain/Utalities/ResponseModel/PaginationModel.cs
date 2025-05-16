using EventBooking.Domain.Enums;

namespace EventBooking.Domain.Utalities.ResponseModel
{
    public class PaginationModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public OrderByDirection OrderByDirection { get; set; }
    }
}
