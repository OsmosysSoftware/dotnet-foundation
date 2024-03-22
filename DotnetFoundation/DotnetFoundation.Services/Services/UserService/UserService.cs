using AutoMapper;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Interfaces.Utility;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;

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

    public async Task<PagedList<UserResponse>> GetAllUsersAsync(PagingRequest pagingRequest)
    {
        PagedList<User> response = await _userRepository.GetAllUsersAsync(pagingRequest).ConfigureAwait(false);

        if (!response.Items.Any())
        {
            throw new NotFoundException($"No data fetched on PageNumber = {response.PageNumber} for PageSize = {response.PageSize}");
        }

        PagedList<UserResponse> pagingResponse = new PagedList<UserResponse>(
            _mapper.Map<List<UserResponse>>(response.Items),
            response.PageNumber,
            response.PageSize,
            response.TotalCount
        );

        return pagingResponse;
    }

    public async Task<UserResponse?> GetUserByIdAsync(int userId)
    {
        User? res = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
        return _mapper.Map<UserResponse>(res);
    }

    public async Task<bool> AddUserRoleAsync(UserRoleRequest request)
    {
        bool res = await _userRepository.AddUserRoleAsync(request.Email, request.Role).ConfigureAwait(false);
        return res;
    }

    public async Task<UserResponse?> DeleteUserAsync(int userId)
    {
        User res = await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false);
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
        User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

        user!.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Country = request.Country;

        await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);
        return _mapper.Map<UserResponse>(user);
    }
}
