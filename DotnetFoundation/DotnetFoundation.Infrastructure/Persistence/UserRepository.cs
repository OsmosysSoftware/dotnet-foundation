using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DotnetFoundation.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly SqlDatabaseContext _dbContext;
    private readonly SignInManager<IdentityApplicationUser> _signInManager;
    private readonly UserManager<IdentityApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserRepository(SqlDatabaseContext sqlDatabaseContext, SignInManager<IdentityApplicationUser> signinManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityApplicationUser> userManager)
    {
        _dbContext = sqlDatabaseContext;
        _roleManager = roleManager;
        _signInManager = signinManager;
        _userManager = userManager;
    }

    public async Task<string> AddUserAsync(RegisterRequest request)
    {
        IdentityApplicationUser newIdentityApplicationUser = new()
        {
            UserName = request.Email,
            Email = request.Email
        };

        IdentityResult identityResult = await _userManager.CreateAsync(newIdentityApplicationUser, request.Password).ConfigureAwait(false);

        if (!identityResult.Succeeded)
        {
            string errorDetails = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            throw new IdentityUserException($"Error creating identity user: {errorDetails}");
        }

        ApplicationUser applicationUser = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Country = request.Country,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            IdentityApplicationUserId = newIdentityApplicationUser.Id
        };

        await _dbContext.ApplicationUsers.AddAsync(applicationUser).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return applicationUser.IdentityApplicationUserId;
    }
    public async Task<List<string>> GetUserRoleAsync(string email)
    {
        IdentityApplicationUser identityApplicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new NotFoundException("Error finding user");
        return (await _userManager.GetRolesAsync(identityApplicationUser).ConfigureAwait(false)).ToList();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return (await _dbContext.ApplicationUsers
            .Where(u => u.Status == Status.ACTIVE)
            .ToListAsync().ConfigureAwait(false))
            .Select(user => new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber
            }).ToList();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        ApplicationUser? user = await _dbContext.ApplicationUsers
            .Where(u => u.Id == userId && u.Status == Status.ACTIVE)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
        return user;
    }
    public async Task<int> UpdateUserAsync(User user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
    public async Task<User> DeleteUserAsync(int userId)
    {
        ApplicationUser? user = await _dbContext.ApplicationUsers
            .Where(u => u.Id == userId && u.Status == Status.ACTIVE)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false) ?? throw new NotFoundException("Error finding user");
        IdentityApplicationUser? identityUser = await _userManager.FindByIdAsync(user.IdentityApplicationUserId!.ToString()).ConfigureAwait(false) ?? throw new NotFoundException("Error finding user");

        // Disable user login
        await _userManager.SetLockoutEnabledAsync(identityUser, true).ConfigureAwait(false);
        await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.MaxValue).ConfigureAwait(false);

        user.Status = Status.INACTIVE;
        _dbContext.Entry(user).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return user;
    }

    public async Task<UserInfo> LoginUserAsync(LoginRequest request)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false).ConfigureAwait(false);
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false) ?? throw new NotFoundException("User does not exist");

        if (!signInResult.Succeeded)
        {
            if (signInResult.IsLockedOut)
            {
                throw new LockoutException("Account is locked out");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false))
            {
                throw new InvalidCredentialsException("Email is not confirmed");
            }
            throw new InvalidCredentialsException("Invalid Email or Password");
        }
        return new(null, user.Id, request.Email, (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList());
    }

    public async Task<string> ForgotPasswordAsync(string email)
    {
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new NotFoundException("Invalid Email");
        int userId = await GetUserIdByIdentityId(user.Id).ConfigureAwait(false);
        _ = await GetUserByIdAsync(userId).ConfigureAwait(false) ?? throw new NotFoundException("No user found with given Email");
        return await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
    }

    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new NotFoundException("Invalid Email");
        int userId = await GetUserIdByIdentityId(user.Id).ConfigureAwait(false);
        _ = await GetUserByIdAsync(userId).ConfigureAwait(false) ?? throw new NotFoundException("No user found with given Email");
        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw new InvalidTokenException("Invalid token");
        }
    }
    public async Task ChangePasswordAsync(string userId, UserChangePassword request)
    {
        IdentityApplicationUser user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false) ?? throw new NotFoundException("Error finding user");
        IdentityResult result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            string errorDetails = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new IdentityUserException($"Error changing user password: {errorDetails}");
        }
        await _signInManager.RefreshSignInAsync(user).ConfigureAwait(false);
    }

    public async Task<bool> AddUserRoleAsync(string email, Roles role)
    {
        string newRole = role.ToString();

        if (!await _roleManager.RoleExistsAsync(newRole).ConfigureAwait(false))
        {
            await _roleManager.CreateAsync(new IdentityRole(newRole)).ConfigureAwait(false);
        }

        IdentityApplicationUser identityApplicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new NotFoundException("Error finding user");
        await _userManager.AddToRoleAsync(identityApplicationUser, newRole).ConfigureAwait(false);
        await _userManager.AddClaimAsync(identityApplicationUser, new Claim(ClaimTypes.Role, newRole)).ConfigureAwait(false);
        return true;
    }

    public async Task<int> GetUserIdByIdentityId(string IdentityId)
    {
        return await _dbContext.ApplicationUsers
            .Where(u => u.IdentityApplicationUserId == IdentityId)
            .Select(u => u.Id)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }

    public async Task<string> GetConfirmationToken(string Id)
    {
        IdentityApplicationUser? user = await _userManager.FindByIdAsync(Id).ConfigureAwait(false) ?? throw new NotFoundException("Error finding user");
        return await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
    }

    public async Task ConfirmEmailAsync(string email, string token)
    {
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new NotFoundException("Error finding user");
        IdentityResult result = await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            throw new InvalidTokenException($"Invalid token");
        }
    }
}