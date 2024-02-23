using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
public record PasswordResetRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; init; }
    [Required(ErrorMessage = "Token is required")]
    public string Token { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }
}
