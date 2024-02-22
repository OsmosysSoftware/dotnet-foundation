namespace DotnetFoundation.Application;

using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Services.Authentication;
using DotnetFoundation.Application.Services.EmailService;
using DotnetFoundation.Application.Services.UserService;
using DotnetFoundation.Application.Services.TaskDetailsService;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITaskDetailsService, TaskDetailsService>();
        return services;
    }
}