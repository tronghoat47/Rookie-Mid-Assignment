namespace BaseProject.WebAPI
{
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var test = context;
            await _next(context);
        }
    }

    // Infrastructure/Middleware/JwtCookieMiddlewareExtensions.cs
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtCookieMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}