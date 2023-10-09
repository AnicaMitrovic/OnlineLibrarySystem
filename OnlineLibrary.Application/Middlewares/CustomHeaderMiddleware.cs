using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace OnlineLibrary.Application.Middlewares
{
    public class CustomHeaderMiddlerware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string? customHeader = context.Request.Headers["custom-header-timestamp"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(customHeader))
            {
                //customHeaderService.CustomHeaderValue = customHeader;
                Console.WriteLine($"My custom header value ID is '{customHeader}'");
            }

            await next(context);
        }
    }
}
