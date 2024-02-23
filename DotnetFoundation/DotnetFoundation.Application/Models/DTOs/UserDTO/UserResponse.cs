namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

public record UserResponse
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string? LastName { get; init; }
}
