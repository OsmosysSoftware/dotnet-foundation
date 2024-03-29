﻿using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Interfaces.Validator;
using DotnetFoundation.Services.Services.Authentication;
using DotnetFoundation.Services.Services.TaskDetailsService;
using DotnetFoundation.Services.Services.UserService;
using DotnetFoundation.Services.Validator;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetFoundation.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Configure service scope for services / BLLs
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskDetailsService, TaskDetailsService>();
        services.AddScoped<ITaskValidator, TaskValidator>();
        services.AddScoped<IUserValidator, UserValidator>();
        return services;
    }
}
