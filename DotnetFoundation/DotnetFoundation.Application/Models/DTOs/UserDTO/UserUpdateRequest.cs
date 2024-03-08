using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

public record UpdateUserRequest
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; init; } = string.Empty;
    public string? LastName { get; init; }

    [MaxLength(50, ErrorMessage = "Country must be at most 50 characters")]
    public string? Country { get; init; }

    [Phone]
    public string? PhoneNumber { get; init; }
}