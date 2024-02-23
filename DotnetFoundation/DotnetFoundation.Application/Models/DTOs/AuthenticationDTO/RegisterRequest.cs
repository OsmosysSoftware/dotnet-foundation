using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
public record RegisterRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }
}