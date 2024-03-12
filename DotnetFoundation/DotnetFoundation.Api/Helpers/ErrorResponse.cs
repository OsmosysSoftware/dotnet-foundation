using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DotnetFoundation.Api.Helpers;
public class ErrorResponse : ActionContextAccessor
{
    public Dictionary<string, List<string>> GetErrorResponse()
    {
        if (ActionContext?.ModelState == null)
        {
            return new Dictionary<string, List<string>>();
        }

        return ActionContext.ModelState
            .Where(modelError => modelError.Value!.Errors.Any())
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(error => error.ErrorMessage).ToList()
            );
    }
}