using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotnetFoundation.Application.Interfaces.Utility;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DotnetFoundation.Infrastructure.Utility;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public JwtTokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetIdentityUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }
    public string GetUserEmail()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    }

    public string GenerateJwtToken(UserInfo user)
    {
        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier,user.IdentityId.ToString()),
            new(ClaimTypes.Email, user.Email),
            new("UserId",user.Id.ToString() ?? string.Empty,ClaimValueTypes.Integer)
        };
        foreach (string role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        string JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new Exception("No JWT configuration");
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(JWT_KEY));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Appsettings:LoginAuthTokenExpiryTimeInHrs"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}