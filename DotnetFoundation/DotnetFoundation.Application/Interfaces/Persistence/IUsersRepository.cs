using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Application.Interfaces.Persistence;

public interface IUserRepository
{
    public Task<string> AddUserAsync(RegisterRequest request);
    public Task<UserInfo> LoginUserAsync(LoginRequest request);
    public Task<List<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(int userId);
    public Task<string> ForgotPasswordAsync(string email);
    public Task ResetPasswordAsync(string email, string token, string newPassword);
    public Task<bool> AddUserRoleAsync(string email, Roles role);
    public Task<User?> UpdateUserAsync(int userId, UpdateUserRequest request);
    public Task<User?> DeleteUserAsync(int userId);
    public Task<List<string>> GetUserRoleAsync(string email);

}