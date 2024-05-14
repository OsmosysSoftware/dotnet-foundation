using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Utility;
using DotnetFoundation.Infrastructure.Identity;
using DotnetFoundation.Infrastructure.Integrations;
using DotnetFoundation.Infrastructure.Persistence;
using DotnetFoundation.Infrastructure.Utility;
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
        // Configure database context
        services.AddDbContext<SqlDatabaseContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Invalid connection string");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        // Configure indentity service
        services.AddIdentity<IdentityApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<SqlDatabaseContext>()
        .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMinutes(Convert.ToDouble(configuration["Appsettings:IdentityTokenLifespanInMinutes"])));

        // Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                string JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new InvalidOperationException("No JWT key specified");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    // ClockSkew will be used for Lifetime validators
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                    {
                        // Clone the validation parameters, and remove the defult lifetime validator
                        TokenValidationParameters clonedParameters = validationParameters.Clone();
                        clonedParameters.LifetimeValidator = null;

                        // If token expiry time is not null, then validate lifetime with skewed clock
                        if (expires != null)
                        {
                            Validators.ValidateLifetime(notBefore, expires, securityToken, clonedParameters);
                        }

                        return true;
                    }
                };
            });

        // Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("ADMIN"));
        });

        // Configure service scope for repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ITaskDetailsRepository, TaskDetailsRepository>();
        services.AddHttpClient();

        return services;
    }
}
