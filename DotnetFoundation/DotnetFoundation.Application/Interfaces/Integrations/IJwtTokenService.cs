using DotnetFoundation.Application.Models.DTOs.UserDTO;

namespace DotnetFoundation.Application.Interfaces.Integrations;

public interface IJwtTokenService
{
    public string GenerateJwtToken(UserInfo user);

};