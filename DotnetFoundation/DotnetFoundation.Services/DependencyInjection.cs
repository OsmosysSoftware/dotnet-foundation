﻿using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Services.Services.Authentication;
using DotnetFoundation.Services.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetFoundation.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Configure service scope for services / BLLs
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
