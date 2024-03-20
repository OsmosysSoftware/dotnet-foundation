using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DotnetFoundation.Infrastructure.Integrations;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(UserInfo user)
    {
        List<Claim> claims = new List<Claim>
        {
            new(ClaimTypes.Email,user.Id.ToString()),
            new(ClaimTypes.Name, user.Email),
            // Add additional claims for roles
        };
        foreach (string role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        string JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new Exception("No JWT configuration");
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
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