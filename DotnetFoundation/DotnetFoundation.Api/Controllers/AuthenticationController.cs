using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> RegisterAsync(RegisterRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);
        
        response.Data = await _authenticationService.RegisterAsync(request).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> LoginAsync(LoginRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);

        response.Data = await _authenticationService.LoginAsync(request).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> ResetPasswordAsync(PasswordResetRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);

        response.Data = await _authenticationService.ResetPasswordAsync(request).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<BaseResponse<string>>> ForgotPasswordAsync(string email)
    {
        BaseResponse<string> response = new(ResponseStatus.Fail);

        response.Data = await _authenticationService.ForgotPasswordAsync(email).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }
}
