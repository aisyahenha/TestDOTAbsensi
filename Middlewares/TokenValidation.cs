using StackExchange.Redis;
using System.Security.Claims;

namespace TestAbsensi.Middlewares
{
    public class TokenValidation
    {
        private readonly RequestDelegate _next;
        private readonly IConnectionMultiplexer _redis;

        public TokenValidation(RequestDelegate next, IConnectionMultiplexer redis)
        {
            _next = next;
            _redis = redis;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var key = ((ClaimsIdentity)context.User.Identity).FindFirst("Username");
                if (token != null)
                {
                    var db = _redis.GetDatabase();
                    var storedToken = await db.StringGetAsync(key?.Value ?? "");

                    if (storedToken.IsNullOrEmpty || storedToken != token)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorized: Invalid or expired token.");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
