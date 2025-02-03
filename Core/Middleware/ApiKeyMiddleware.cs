namespace MovieManagementApi.Core.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_SECRET_KEY = "123456";  // Hardcoded secret API key

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("X-API-KEY"))
            {
                var apiKey = context.Request.Headers["X-API-KEY"];
                if (apiKey != API_SECRET_KEY)
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