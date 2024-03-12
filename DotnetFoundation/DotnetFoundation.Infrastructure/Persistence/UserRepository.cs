using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DotnetFoundation.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly SqlDatabaseContext _dbContext;
    private readonly SignInManager<IdentityApplicationUser> _signInManager;
    private readonly UserManager<IdentityApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IActionContextAccessor _actionContextAccessor;
    public UserRepository(SqlDatabaseContext sqlDatabaseContext, SignInManager<IdentityApplicationUser> signinManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityApplicationUser> userManager, IActionContextAccessor actionContextAccessor)
    {
        _dbContext = sqlDatabaseContext;
        _roleManager = roleManager;
        _signInManager = signinManager;
        _userManager = userManager;
        _actionContextAccessor = actionContextAccessor;
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
            List<string> errorDetails = identityResult.Errors.Select(e => e.Description).ToList();
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("email", string.Join(", ", errorDetails));
            throw new IdentityUserException(ErrorValues.GenericUserErrorMessage);
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
        IdentityApplicationUser? identityApplicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
        if (identityApplicationUser == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("email", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
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
            .ConfigureAwait(false);

        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("userId", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }

        IdentityApplicationUser? identityUser = await _userManager.FindByIdAsync(user.IdentityApplicationUserId!.ToString()).ConfigureAwait(false);
        if (identityUser == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("userId", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }

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

        if (!signInResult.Succeeded)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("request", "Invalid Email or Password");
            throw new InvalidCredentialsException(ErrorValues.GenericInvalidCredentialsMessage);
        }
        IdentityApplicationUser? user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("email", "Invalid Email or password");
            throw new InvalidCredentialsException(ErrorValues.GenericInvalidCredentialsMessage);
        }
        return new(user.Id, request.Email, (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList());
    }

    public async Task<string> ForgotPasswordAsync(string email)
    {
        IdentityApplicationUser? user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("email", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
        return await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
    }
    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        IdentityApplicationUser? user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("email", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            List<string> errorDetails = result.Errors.Select(e => e.Description).ToList();
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("token", string.Join(", ", errorDetails));
            throw new InvalidTokenException(ErrorValues.GenericInvalidTokenErrorMessage);
        }
    }
    public async Task<bool> AddUserRoleAsync(string email, Roles role)
    {
        string newRole = role.ToString();

        if (!await _roleManager.RoleExistsAsync(newRole).ConfigureAwait(false))
        {
            await _roleManager.CreateAsync(new IdentityRole(newRole)).ConfigureAwait(false);
        }

        IdentityApplicationUser? identityApplicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
        if (identityApplicationUser == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("email", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
        await _userManager.AddToRoleAsync(identityApplicationUser, newRole).ConfigureAwait(false);
        await _userManager.AddClaimAsync(identityApplicationUser, new Claim(ClaimTypes.Role, newRole)).ConfigureAwait(false);
        return true;
    }
}