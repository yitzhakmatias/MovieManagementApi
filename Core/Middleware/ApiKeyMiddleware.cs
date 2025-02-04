using Microsoft.Extensions.Options;
using MovieManagementApi.Core.Entities;

namespace MovieManagementApi.Core.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private static string _apiSecretKey = "123456";

        public ApiKeyMiddleware(RequestDelegate next,IOptions<AppSettings> options)
        {
            _next = next;
            _apiSecretKey=options.Value.API_SECRET_KEY; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("X-API-KEY"))
            {
                var apiKey = context.Request.Headers["X-API-KEY"];
                if (apiKey != _apiSecretKey)
                {
                    // Unauthorized if the API Key is incorrect
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid API Key");
                    return;
                }
            }
            else
            {
                // Bad Request if no API Key is provided
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("API Key is required");
                return;
            }

            // Continue processing the request if the API Key is valid
            await _next(context);
        }
    }
}