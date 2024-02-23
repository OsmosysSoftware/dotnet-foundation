using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Services.Authentication;
using DotnetFoundation.Application.Services.EmailService;
using DotnetFoundation.Application.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetFoundation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Configure service scope for services / BLLs
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
