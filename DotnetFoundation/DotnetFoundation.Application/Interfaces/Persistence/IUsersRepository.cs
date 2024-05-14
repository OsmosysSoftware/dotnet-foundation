using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Application.Interfaces.Persistence;

public interface IUserRepository
{
    public Task<string> AddUserAsync(RegisterRequest request);
    public Task<UserInfo> LoginUserAsync(LoginRequest request);
    public Task<PagedList<User>> GetAllUsersAsync(PagingRequest pagingRequest);
    public Task<User?> GetUserByIdAsync(int userId);
    public Task<string> ForgotPasswordAsync(string email);
    public Task ResetPasswordAsync(string email, string token, string newPassword);
    public Task<bool> AddUserRoleAsync(string email, Roles role);
    public Task<int> UpdateUserAsync(User user);
    public Task<string> GetConfirmationToken(string Id);
    public Task ConfirmEmailAsync(string email, string token);
    public Task<User> DeleteUserAsync(int userId);
    public Task<List<string>> GetUserRoleAsync(string email);
    public Task<bool> CheckEmailExist(string email);
    public Task<bool> CheckEmailRegistered(string email);
    public Task<int> GetUserIdByIdentityId(string IdentityId);
    public Task ChangePasswordAsync(string userId, UserChangePassword request);
}