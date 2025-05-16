using System.Net;

namespace EventBooking.Domain.Utalities.ResponseModel
{
    public class ResponsePagination<TData> : Response<TData> where TData : class
    {
        public ResponsePagination(HttpStatusCode statusCode = default,
            bool issucceeded = default,
            string message = default,
            string errors = default,
            string meta = default,
            TData? data = default,
            int totalCount = 0,
            int currentPage = 1,
            int pageSize = 10) : base(statusCode, issucceeded, message, errors, meta, data)
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;
    }
}
