using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Services.Services.Authentication;
using DotnetFoundation.Services.Services.TaskDetailsService;
using DotnetFoundation.Services.Services.UserService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetFoundation.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Configure service scope for services / BLLs
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskDetailsService, TaskDetailsService>();

        return services;
    }
}
