using System.IdentityModel.Tokens.Jwt;
using DbWebApplication.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DbWebApplication.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeByRole : Attribute, IAuthorizationFilter
{
    private readonly Role[] _roles;

    public AuthorizeByRole(params Role[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Cookies["tastkook"];
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var jwtHandler = new JwtSecurityTokenHandler();
        if (!jwtHandler.CanReadToken(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var jwtToken = jwtHandler.ReadJwtToken(token);
        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
        if (roleClaim == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var userRole = (Role)System.Enum.Parse(typeof(Role), roleClaim.Value);
        if (!_roles.Contains(userRole))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}