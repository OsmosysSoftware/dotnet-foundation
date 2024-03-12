using DotnetFoundation.Application.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Application.Models.Common;

public static class ErrorValues
{
    public static readonly string GenricValidationMessage = "Generic Validation Error";
    public static readonly string GenricNotFoundMessage = "Generic Not Found Error";
    public static readonly string InternalServerError = "An internal server error has occured";
    public static readonly string GenericUserErrorMessage = "Generic User Error";
    public static readonly string GenericInvalidCredentialsMessage = "Invalid Credentials";
    public static readonly string GenericInvalidTokenErrorMessage = "Invalid Token";

}
public class BaseResponse<T>
{
    public BaseResponse(ResponseStatus status) => Status = status;
    public ResponseStatus? Status { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
    public Dictionary<string, List<string>>? Errors { get; set; }
}

public class ModelValidationBadRequest
{
    public static BadRequestObjectResult ModelValidationErrorResponse(ActionContext actionContext)
    {
        return new BadRequestObjectResult(new BaseResponse<int>(ResponseStatus.Error)
        {
            Errors = actionContext.ModelState
                .Where(modelError => modelError.Value.Errors.Any())
                .ToDictionary(
                    modelError => modelError.Key,
                    modelError => modelError.Value.Errors.Select(e => e.ErrorMessage).ToList()
                )
        });
    }
}
