using DotnetFoundation.Application.Models.Common;
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

        return services;
    }
}
