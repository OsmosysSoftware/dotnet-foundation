namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
public record PasswordResetRequest(string Email, string Token, string Password);