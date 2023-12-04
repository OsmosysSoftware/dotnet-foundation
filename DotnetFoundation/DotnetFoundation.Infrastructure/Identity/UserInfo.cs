namespace DotnetFoundation.Infrastructure.Identity;
public record UserInfo(string Id, string Email, List<string> Roles);