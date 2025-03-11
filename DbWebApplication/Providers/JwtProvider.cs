using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DbWebApplication.Interfaces;
using DbWebApplication.Models;
using Microsoft.IdentityModel.Tokens;

namespace DbWebApplication.Providers;

public class JwtTokenProvider(IConfiguration configuration) : IJwtProvider
{
    public string Created(AppUserModel userModel)
    {
        string jwtSecret = configuration["JwtSettings:Key"];
        DateTime expires = DateTime.Now.AddHours(int.Parse(configuration["JwtSettings:ExpireHours"]));
        var simetrikSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        
        var signInCredentials = new SigningCredentials(simetrikSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Email),
            new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
            new("userId", userModel.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = signInCredentials,
            Issuer = configuration["JwtSettings:Issuer"],
            Audience = configuration["JwtSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);
        
        return tokenString;
    }
}