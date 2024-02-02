using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Infrastructure.Identity;
using DotnetFoundation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotnetFoundation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<SqlDatabaseContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("DBConnection") ?? throw new Exception("Invalid connection string");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        services.AddIdentity<IdentityApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<SqlDatabaseContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {

            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            string JWT_KEY = configuration["Jwt:Key"] ?? throw new Exception("No JWT key specified");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY))
            };

        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("ADMIN"));
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}