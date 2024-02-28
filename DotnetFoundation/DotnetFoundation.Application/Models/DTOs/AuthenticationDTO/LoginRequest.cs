using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;

/// <summary>
/// Represents a request for user login.
/// </summary>
public record LoginRequest
{
    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; init; } = null!;
    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$",
        ErrorMessage = "8-10 character length with 1 uppercase, 1 lowercase, 1 number, 1 special character is required")]
    public string Password { get; init; } = null!;
}
