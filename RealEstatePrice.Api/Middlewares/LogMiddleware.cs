using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;

namespace RealEstatePrice.Api.Middlewares
{
    /// <summary>
    /// Log 中介層
    /// 將系統上所有的 Log 統一在這個物件上處理。
    /// </summary>
    public class LogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetLogger("ApiLog");
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            byte[] buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            string requestBody = Encoding.UTF8.GetString(buffer);
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            StringBuilder builder = new StringBuilder(Environment.NewLine);
            List<string> skipUrl = new List<string> {"SIGN-IN"};

            bool isSkipBody = skipUrl.Any(x => x == context.Request.Path.Value.Split('/').Last().ToUpper());
            string body =
                $"{context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Request.QueryString}";
            if (!isSkipBody) body += $"Request body:{requestBody}";
            builder.AppendLine(body);

            Stream originalBodyStream = context.Response.Body;
            using (MemoryStream responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                string response = await FormatResponse(context.Response);
                builder.AppendLine($"Response: {response}");
                _logger.Info(builder.ToString());
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode} {text}";
        }
    }
}