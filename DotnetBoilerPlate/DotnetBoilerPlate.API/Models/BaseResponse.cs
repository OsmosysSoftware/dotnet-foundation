using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerPlate.API.Models
{
    public enum ResponseStatus
    {
        Success,
        Fail,
        Error
    }

    public enum ErrorCode
    {
        INTERNAL_SERVER_ERROR,
        USER_ID_REQUIRED,
        USER_NOT_FOUND
    }

    public class BaseResponse<T>
    {
        public BaseResponse(ResponseStatus status) => Status = status;
        public ResponseStatus? Status { get; set; }
        public T? Data { get; set; }
        public ErrorCode? ErrorCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }

    public class ModelValidationBadRequest
    {
        public static BadRequestObjectResult ModelValidationErrorResponse(ActionContext actionContext)
        {
            return new BadRequestObjectResult(actionContext.ModelState
                .Where(modelError => modelError.Value.Errors.Count > 0)
                .Select(modelError => new BaseResponse<int>(ResponseStatus.Error)
                {
                    ErrorCode = Enum.TryParse(modelError.Value.Errors.FirstOrDefault().ErrorMessage, out ErrorCode enumErrorCode) ? enumErrorCode : null,
                    Message = modelError.Value.Errors.FirstOrDefault().ErrorMessage
                }).FirstOrDefault());
        }
    }
}
