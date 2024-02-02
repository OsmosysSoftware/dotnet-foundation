using DotnetFoundation.Application.DTO.UserDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Application.Services.UserService;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private static UserResponse DTOMapper(User user)
    {
        return new(Id: user.Id, FirstName: user.FirstName, LastName: user.LastName);
    }
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        List<UserResponse> response = (await _userRepository.GetAllUsersAsync()).Select(DTOMapper).ToList();
        return response;
    }

    public async Task<UserResponse?> GetUserByIdAsync(int Id)
    {
        User res = await _userRepository.GetUserByIdAsync(Id) ?? throw new Exception("No user found");
        return DTOMapper(res);
    }

    public async Task<bool> AddUserRoleAsync(string email, int role)
    {
        bool res = await _userRepository.AddUserRoleAsync(email, role);
        return res;
    }
}