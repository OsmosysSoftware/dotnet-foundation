using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.Enums;
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

    /// <summary>
    /// User registration.
    /// </summary>
    /// <param name="request">New user registration request</param>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> RegisterAsync(RegisterRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);

        response.Data = await _authenticationService.RegisterAsync(request).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// User login.
    /// </summary>
    /// <param name="request">User login request</param>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> LoginAsync(LoginRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);

        response.Data = await _authenticationService.LoginAsync(request).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// User password reset using reset token.
    /// </summary>
    /// <param name="request">New password details request</param>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> ResetPasswordAsync(PasswordResetRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);

        response.Data = await _authenticationService.ResetPasswordAsync(request).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Forgot user password.
    /// </summary>
    /// <param name="email">Email of user to reset password</param>
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<string>>> ForgotPasswordAsync(string email)
    {
        BaseResponse<string> response = new(ResponseStatus.Fail);

        response.Data = await _authenticationService.ForgotPasswordAsync(email).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }
}
