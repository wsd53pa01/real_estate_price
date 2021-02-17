using System.Collections.Generic;

namespace RealEstatePrice.Core.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null, string statusCode = nameof(StatusCodeOptions.S001))
        {
            Succeeded = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
            StatusCode = nameof(StatusCodeOptions.S001);
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public T Data { get; set; }
    }
}