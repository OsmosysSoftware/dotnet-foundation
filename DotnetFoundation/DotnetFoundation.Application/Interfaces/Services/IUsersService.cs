using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.UserDTO;

namespace DotnetFoundation.Application.Interfaces.Services;

public interface IUserService
{
    public Task<UserResponse?> GetUserByIdAsync(int userId);
    public Task<PagedList<UserResponse>> GetAllUsersAsync(PagingRequest pagingRequest);
    public Task<bool> AddUserRoleAsync(UserRoleRequest request);
    public Task<UserResponse?> UpdateUserAsync(int userId, UpdateUserRequest request);
    public Task<UserResponse?> DeleteUserAsync(int userId);
    public Task ChangePasswordAsync(UserChangePassword request);
}