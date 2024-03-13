using DotnetFoundation.Application.Models.DTOs.UserDTO;

namespace DotnetFoundation.Application.Interfaces.Services;

public interface IUserService
{
    public Task<UserResponse?> GetUserByIdAsync(int userId);
    public Task<List<UserResponse>> GetAllUsersAsync();
    public Task<bool> AddUserRoleAsync(UserRoleRequest request);
    public Task<UserResponse?> UpdateUserAsync(int userId, UpdateUserRequest request);
    public Task<UserResponse?> DeleteUserAsync(int userId);
}