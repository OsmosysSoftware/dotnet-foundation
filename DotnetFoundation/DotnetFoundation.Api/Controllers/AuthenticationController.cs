using DotnetFoundation.Application.DTO.AuthenticationDTO;
using DotnetFoundation.Application.Interfaces.Services;
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
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        AuthenticationResponse result = await _authenticationService.RegisterAsync(request).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        AuthenticationResponse result = await _authenticationService.LoginAsync(request).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(PasswordResetRequest request)
    {
        AuthenticationResponse result = await _authenticationService.ResetPasswordAsync(request).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(string email)
    {
        string result = await _authenticationService.ForgotPasswordAsync(email).ConfigureAwait(false);
        return Ok(result);
    }
}