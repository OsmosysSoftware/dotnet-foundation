using DotnetFoundation.Application.Models.DTOs.UserDTO;

namespace DotnetFoundation.Application.Interfaces.Utility;

public interface IJwtTokenService
{
    public string GenerateJwtToken(UserInfo user);
    public string GetIdentityUserId();
    public string GetUserEmail();

};