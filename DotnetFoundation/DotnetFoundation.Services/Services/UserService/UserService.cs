using AutoMapper;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DotnetFoundation.Services.Services.UserService;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IActionContextAccessor _actionContextAccessor;

    public UserService(IUserRepository userRepository, IMapper mapper, IActionContextAccessor actionContextAccessor)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _actionContextAccessor = actionContextAccessor;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        List<User> users = await _userRepository.GetAllUsersAsync().ConfigureAwait(false);
        return _mapper.Map<List<UserResponse>>(users);
    }

    public async Task<UserResponse?> GetUserByIdAsync(int userId)
    {
        User? res = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
        if (res == null)
        {
            _actionContextAccessor.ActionContext.ModelState.AddModelError("UserId", "User Not Found");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
        return _mapper.Map<UserResponse>(res);
    }

    public async Task<bool> AddUserRoleAsync(string email, Roles role)
    {
        bool res = await _userRepository.AddUserRoleAsync(email, role).ConfigureAwait(false);
        return res;
    }

    public async Task<UserResponse?> DeleteUserAsync(int userId)
    {
        User res = await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false);
        if (res == null)
        {
            _actionContextAccessor.ActionContext.ModelState.AddModelError("UserId", "User Not Found");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
        return _mapper.Map<UserResponse>(res);
    }

    public async Task<UserResponse?> UpdateUserAsync(int userId, UpdateUserRequest request)
    {
        User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
        if (user == null)
        {
            _actionContextAccessor.ActionContext.ModelState.AddModelError("UserId", "No user Found");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Country = request.Country;

        await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);
        return _mapper.Map<UserResponse>(user);
    }
}
