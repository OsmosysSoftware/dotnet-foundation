using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;

namespace DotnetFoundation.Application.Interfaces.Services;

public interface IAuthenticationService
{
    public Task<AuthenticationResponse> RegisterAsync(RegisterRequest request);
    public Task<AuthenticationResponse> LoginAsync(LoginRequest request);
    public Task ForgotPasswordAsync(string email);
    public Task ResetPasswordAsync(PasswordResetRequest request);
    public Task ConfirmEmailAsync(ConfirmEmailRequest request);
}