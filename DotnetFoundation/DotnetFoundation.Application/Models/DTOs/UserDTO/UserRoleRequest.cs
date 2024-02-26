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
    public string Email { get; init; }
    
    /// <summary>
    /// Gets the role to be assigned to the user.
    /// </summary>
    public Roles Role { get; init; }
}
