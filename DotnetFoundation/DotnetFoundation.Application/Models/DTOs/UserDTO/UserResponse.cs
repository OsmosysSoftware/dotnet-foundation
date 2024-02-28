namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

/// <summary>
/// Represents a response containing user information.
/// </summary>
public record UserResponse
{
    /// <summary>
    /// Gets the user's identifier.
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Gets the user's first name.
    /// </summary>
    public string FirstName { get; init; } = String.Empty;
    /// <summary>
    /// Gets the user's last name.
    /// </summary>
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? Country { get; init; }
    public string? PhoneNumber { get; init; }
}
