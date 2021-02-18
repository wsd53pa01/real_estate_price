using System;
using System.Collections.Generic;

namespace RealEstatePrice.Core.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null, StatusCodeOptions statusCode = StatusCodeOptions.S001, bool succeeded = true)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
            StatusCode = Enum.GetName(typeof(StatusCodeOptions), statusCode);
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
            StatusCode = nameof(StatusCodeOptions.E001);
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public T Data { get; set; }
    }
}