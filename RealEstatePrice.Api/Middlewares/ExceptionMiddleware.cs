using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RealEstatePrice.Api.Middlewares
{
    /// <summary>
    /// Exception 中介層，
    /// 將系統上所有的 Exception 統一在這個物件上處理。
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string request = await FormatRequest(context.Request); // save to log file
                context.Response.ContentType = "application/json";
                string json = @"{ ""IsSuccess"": false, ""Message"": ""Internal Server Error"", ""Data"": {} }";
                await context.Response.WriteAsync(json);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> FormatRequest(HttpRequest request)
        {
            Stream body = request.Body;

            byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            string bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText} \n {request}";
        }
    }
}