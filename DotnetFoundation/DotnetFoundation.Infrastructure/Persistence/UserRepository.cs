using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using DotnetFoundation.Infrastructure.Identity;
using DotnetFoundation.Infrastructure.Integrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace DotnetFoundation.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly SqlDatabaseContext _dbContext;
    private readonly SignInManager<IdentityApplicationUser> _signInManager;
    private readonly UserManager<IdentityApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailService _emailService;
    private readonly IJwtTokenService _jwtService;
    public UserRepository(IConfiguration configuration, SqlDatabaseContext sqlDatabaseContext, SignInManager<IdentityApplicationUser> signinManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityApplicationUser> userManager, IEmailService emailService, IJwtTokenService jwtService)
    {
        _dbContext = sqlDatabaseContext;
        _configuration = configuration;
        _roleManager = roleManager;
        _signInManager = signinManager;
        _userManager = userManager;
        _emailService = emailService;
        _jwtService = jwtService;
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
            throw new Exception("Error creating identity user");
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
        IdentityApplicationUser identityApplicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Error finding user");
        return (await _userManager.GetRolesAsync(identityApplicationUser).ConfigureAwait(false)).ToList();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User> users = (await _dbContext.ApplicationUsers
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
        return users;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        ApplicationUser? user = await _dbContext.ApplicationUsers
                                                    .Where(u => u.Id == userId && u.Status == Status.ACTIVE)
                                                    .FirstOrDefaultAsync()
                                                    .ConfigureAwait(false);
        return user;
    }
    public async Task<User?> UpdateUserAsync(int userId, UpdateUserRequest request)
    {

        ApplicationUser? user = await _dbContext.ApplicationUsers.FindAsync(userId).ConfigureAwait(false);
        if (user == null || user.Status != Status.ACTIVE)
        {
            return null; // User not found
        }

        // Validate and update properties
        foreach (System.Reflection.PropertyInfo property in typeof(UpdateUserRequest).GetProperties())
        {
            string? requestValue = property.GetValue(request)?.ToString();
            if (!string.IsNullOrEmpty(requestValue))
            {
                typeof(ApplicationUser).GetProperty(property.Name)?.SetValue(user, requestValue);
            }
        }

        _dbContext.Entry(user).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return new User { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Country = user.Country, PhoneNumber = user.PhoneNumber };
    }
    public async Task<User?> DeleteUserAsync(int userId)
    {
        ApplicationUser? user = await _dbContext.ApplicationUsers.FindAsync(userId).ConfigureAwait(false);
        if (user == null)
        {
            return null; // User not found
        }

        IdentityApplicationUser? identityUser = await _userManager.FindByIdAsync(user.IdentityApplicationUserId!.ToString()).ConfigureAwait(false);
        if (identityUser == null)
        {
            return null; // User not found
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
            throw new Exception("Invalid Email or Password");
        }
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false) ?? throw new Exception("User does not exist");
        return new(user.Id, request.Email, (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList());
    }

    public async Task<string> ForgotPasswordAsync(string email)
    {
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Invalid Email");
        return await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
    }

    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Invalid Email");
        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw new Exception("Invalid token");
        }
    }

    public async Task<bool> AddUserRoleAsync(string email, Roles role)
    {
        string newRole = role.ToString();

        if (!await _roleManager.RoleExistsAsync(newRole).ConfigureAwait(false))
        {
            await _roleManager.CreateAsync(new IdentityRole(newRole)).ConfigureAwait(false);
        }

        IdentityApplicationUser identityApplicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Error finding user");
        await _userManager.AddToRoleAsync(identityApplicationUser, newRole).ConfigureAwait(false);
        await _userManager.AddClaimAsync(identityApplicationUser, new Claim(ClaimTypes.Role, newRole)).ConfigureAwait(false);
        return true;
    }
}