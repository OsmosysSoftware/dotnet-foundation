using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Services.Authentication;
using DotnetFoundation.Application.Services.EmailService;
using DotnetFoundation.Application.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetFoundation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Configure Error Response from Model Validations
        services.AddMvc().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                return ModelValidationBadRequest.ModelValidationErrorResponse(actionContext);
            };
        });

        // Configure service scope for services / BLLs
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
