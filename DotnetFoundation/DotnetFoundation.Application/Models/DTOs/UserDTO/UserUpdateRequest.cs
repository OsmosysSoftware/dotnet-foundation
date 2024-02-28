namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

public record UpdateUserRequest(string? FirstName, string? LastName, string? Country, string? PhoneNumber);