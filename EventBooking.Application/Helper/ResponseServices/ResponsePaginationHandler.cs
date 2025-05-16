using EventBooking.Application.Constants;
using EventBooking.Domain.Utalities.ResponseModel;
using System.Net;

namespace EventBooking.Application.Helper.ResponseServices
{
    public sealed class ResponsePaginationHandler
    {
        public static ResponsePagination<TData> Success<TData>(
           TData data = null,
           string message = null,
           string meta = null,
           string errors = null,
           int totalCount = 0,
           int pageNumber = 1,
           int pageSize = 10) where TData : class
        {
            return new ResponsePagination<TData>(
                statusCode: HttpStatusCode.OK,
                issucceeded: true,
                message: message ?? ResponseMessage.SuccessMessage,
                meta: meta,
                data: data,
                errors: errors ?? "There are no errore",
                totalCount: totalCount,
                currentPage: pageNumber,
                pageSize: pageSize

            );
        }

        public static ResponsePagination<TData> NotFound<TData>(
            string message = null,
            string meta = null,
            string errors = null,
            TData data = null,
            int totalCount = 0,
            int pageNumbre = 1,
            int pageSize = 10) where TData : class
        {
            return new ResponsePagination<TData>(
                statusCode: HttpStatusCode.NotFound,
                issucceeded: true,
                message: message ?? ResponseMessage.NotFoundMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Not found data",
                totalCount: totalCount,
                currentPage: pageNumbre,
                pageSize: pageSize

            );
        }

        public static ResponsePagination<TData> BadRequest<TData>(
            string message = null,
            string meta = null,
            string errors = null,
            TData data = null,
            int totalCount = 0,
            int pageNumbre = 1,
            int pageSize = 10) where TData : class
        {
            return new ResponsePagination<TData>(
                statusCode: HttpStatusCode.BadRequest,
                issucceeded: true,
                message: message ?? ResponseMessage.BadRequestMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Bad Request",
                totalCount: totalCount,
                currentPage: pageNumbre,
                pageSize: pageSize

            );
        }
    }
}
