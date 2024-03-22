using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

public record UserChangePassword
{
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$",
        ErrorMessage = "8-10 character length with 1 uppercase, 1 lowercase, 1 number, 1 special character is required")]
    public string CurrentPassword { get; init; } = String.Empty;

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$",
        ErrorMessage = "8-10 character length with 1 uppercase, 1 lowercase, 1 number, 1 special character is required")]
    public string NewPassword { get; init; } = String.Empty;
}
