using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

public record UserRoleRequest
{
    public string Email { get; init; }
    public Roles Role { get; init; }
}
