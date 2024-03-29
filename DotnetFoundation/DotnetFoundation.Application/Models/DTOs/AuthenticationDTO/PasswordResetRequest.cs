using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
public record PasswordResetRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; init; } = String.Empty;
    [Required(ErrorMessage = "Token is required")]
    public string Token { get; init; } = String.Empty;
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$",
        ErrorMessage = "8-10 character length with 1 uppercase, 1 lowercase, 1 number, 1 special character is required")]
    public string Password { get; init; } = String.Empty;
}
