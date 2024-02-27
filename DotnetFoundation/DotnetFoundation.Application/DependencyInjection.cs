using DotnetFoundation.Application.Models.Common;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetFoundation.Application;

/// <summary>
/// Configures services for the application layer.
/// </summary>
/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
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
