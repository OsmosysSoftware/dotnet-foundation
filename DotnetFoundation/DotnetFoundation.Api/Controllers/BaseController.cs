using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

public class BaseController : ControllerBase
{
    protected Dictionary<string, List<string>> GetErrorResponse()
    {
        return ModelState
            .Where(modelError => modelError.Value!.Errors.Any())
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(error => error.ErrorMessage).ToList()
            );
    }
}
