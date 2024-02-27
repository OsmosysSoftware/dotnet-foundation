namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;

/// <summary>
/// Represents the response for authentication-related actions.
/// </summary>
public record AuthenticationResponse
{
    /// <summary>
    /// Gets the authentication token.
    /// </summary>
    public string Token { get; init; }
}
