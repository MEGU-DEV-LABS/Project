using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");

                if (userIdClaim != null && roleClaim != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userIdClaim.Value),
                        new Claim(ClaimTypes.Role, roleClaim.Value),
                    };

                    var identity = new ClaimsIdentity(claims, "custom");
                    var principal = new ClaimsPrincipal(identity);

                    context.User = principal; 
                }
            }
        }

        await _next(context);
    }
}