namespace DotnetFoundation.Application.Models.DTOs.UserDTO;
public record UserInfo
{
    public int? Id { get; set; }
    public string IdentityId { get; init; } = String.Empty;
    public string Email { get; init; } = String.Empty;
    public List<string> Roles { get; init; } = null!;

    public UserInfo(int? id, string identityId, string email, List<string> roles)
    {
        Id = id;
        IdentityId = identityId;
        Email = email;
        Roles = roles;
    }
}