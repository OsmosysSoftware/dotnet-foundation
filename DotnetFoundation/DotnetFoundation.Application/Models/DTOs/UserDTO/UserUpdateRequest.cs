using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

public record UpdateUserRequest
{
    [Required(ErrorMessage = "FirstName is required")]
    [MaxLength(100, ErrorMessage = "FirstName must be at most 100 characters")]
    public string FirstName { get; init; } = string.Empty;

    [Required(ErrorMessage = "LastName is required")]
    [MaxLength(100, ErrorMessage = "LastName must be at most 100 characters")]
    public string? LastName { get; init; }

    [Required(ErrorMessage = "Country is required")]
    [MaxLength(50, ErrorMessage = "Country must be at most 50 characters")]
    public string? Country { get; init; }

    [Phone]
    public string? PhoneNumber { get; init; }
}