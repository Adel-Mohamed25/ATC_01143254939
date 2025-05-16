using System.Net;

namespace EventBooking.Domain.Utalities.ResponseModel
{
    public class Response<TData> where TData : class
    {
        public Response() { }

        public Response(HttpStatusCode statusCode = default,
           bool issucceeded = default,
           string message = default,
           string errors = default,
           string meta = default,
           TData? data = default)
        {
            StatusCode = statusCode;
            IsSucceeded = issucceeded;
            Message = message;
            Errors = errors;
            Meta = meta;
            Data = data;

        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public string Meta { get; set; }
        public TData? Data { get; set; }
    }
}
