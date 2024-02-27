using AutoMapper;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Services.Services.UserService;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        List<User> users = await _userRepository.GetAllUsersAsync().ConfigureAwait(false);
        return _mapper.Map<List<UserResponse>>(users);
    }

    public async Task<UserResponse?> GetUserByIdAsync(int Id)
    {
        User res = await _userRepository.GetUserByIdAsync(Id).ConfigureAwait(false) ?? throw new Exception("No user found");
        return _mapper.Map<UserResponse>(res);
    }

    public async Task<bool> AddUserRoleAsync(string email, Roles role)
    {
        bool res = await _userRepository.AddUserRoleAsync(email, role).ConfigureAwait(false);
        return res;
    }
}
