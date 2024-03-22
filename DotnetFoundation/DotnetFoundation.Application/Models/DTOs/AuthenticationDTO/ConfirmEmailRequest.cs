using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;

public class ConfirmEmailRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; init; } = String.Empty;
    [Required(ErrorMessage = "Token is required")]
    public string Token { get; init; } = String.Empty;
}