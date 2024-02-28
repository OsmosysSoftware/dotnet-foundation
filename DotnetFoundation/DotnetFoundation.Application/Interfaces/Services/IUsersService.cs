using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Enums;
namespace DotnetFoundation.Application.Interfaces.Services;

public interface IUserService
{
    public Task<UserResponse?> GetUserByIdAsync(int userId);
    public Task<List<UserResponse>> GetAllUsersAsync();
    public Task<bool> AddUserRoleAsync(string email, Roles role);
    public Task<UserResponse?> UpdateUserAsync(int userId, UpdateUserRequest request);
    public Task<bool> DeleteUserAsync(int userId);
}