using AutoMapper;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Interfaces.Utility;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Services.Services.UserService;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    public UserService(IUserRepository userRepository, IMapper mapper, IJwtTokenService jwtService, IEmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
        _emailService = emailService;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        List<User> users = await _userRepository.GetAllUsersAsync().ConfigureAwait(false);
        return _mapper.Map<List<UserResponse>>(users);
    }

    public async Task<UserResponse?> GetUserByIdAsync(int userId)
    {
        User res = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false) ?? throw new NotFoundException("No user found");
        return _mapper.Map<UserResponse>(res);
    }

    public async Task<bool> AddUserRoleAsync(string email, Roles role)
    {
        bool res = await _userRepository.AddUserRoleAsync(email, role).ConfigureAwait(false);
        return res;
    }

    public async Task<UserResponse?> DeleteUserAsync(int userId)
    {
        User res = await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false) ?? throw new NotFoundException("No user found");
        return _mapper.Map<UserResponse>(res);
    }
    public async Task ChangePasswordAsync(UserChangePassword request)
    {
        string userId = _jwtService.GetIdentityUserId();
        string email = _jwtService.GetUserEmail();
        await _userRepository.ChangePasswordAsync(userId, request).ConfigureAwait(false);
        await _emailService.SendChangePasswordEmailAsync(email).ConfigureAwait(false);
    }

    public async Task<UserResponse?> UpdateUserAsync(int userId, UpdateUserRequest request)
    {
        User user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false) ?? throw new NotFoundException("No user found");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Country = request.Country;

        int affectedRows = await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);
        if (affectedRows == 0)
        {
            throw new UserException("Error updating user");
        }
        return _mapper.Map<UserResponse>(user);
    }
}
