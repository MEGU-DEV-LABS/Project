using System.IdentityModel.Tokens.Jwt;

namespace DbWebApplication.Middlewares;

public class UserIdMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Cookies["tastkook"];
        if (!string.IsNullOrEmpty(token))
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (jwtHandler.CanReadToken(token))
            {
                var jwtToken = jwtHandler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId");

                if (userIdClaim != null)
                {
                    context.Items["UserId"] = int.Parse(userIdClaim.Value);
                }
            }
        }

        await _next(context);
    }
}