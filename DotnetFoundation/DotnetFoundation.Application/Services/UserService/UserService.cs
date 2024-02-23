using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Application.Services.UserService;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private static UserResponse DTOMapper(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        List<UserResponse> response = (await _userRepository.GetAllUsersAsync().ConfigureAwait(false)).Select(DTOMapper).ToList();
        return response;
    }

    public async Task<UserResponse?> GetUserByIdAsync(int Id)
    {
        User res = await _userRepository.GetUserByIdAsync(Id).ConfigureAwait(false) ?? throw new Exception("No user found");
        return DTOMapper(res);
    }

    public async Task<bool> AddUserRoleAsync(string email, Roles role)
    {
        bool res = await _userRepository.AddUserRoleAsync(email, role).ConfigureAwait(false);
        return res;
    }
}