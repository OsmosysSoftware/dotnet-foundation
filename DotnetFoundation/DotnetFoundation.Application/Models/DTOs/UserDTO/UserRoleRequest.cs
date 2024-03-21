using System.ComponentModel.DataAnnotations;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

/// <summary>
/// Represents a request to assign a role to a user.
/// </summary>
public record UserRoleRequest
{
    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; init; } = String.Empty;

    /// <summary>
    /// Gets the role to be assigned to the user.
    /// </summary>
    [Required(ErrorMessage = "Role is required")]
    public Roles Role { get; init; }
}
