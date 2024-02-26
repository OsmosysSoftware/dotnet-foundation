using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
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
    public UserRepository(IConfiguration configuration, SqlDatabaseContext sqlDatabaseContext, SignInManager<IdentityApplicationUser> signinManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityApplicationUser> userManager, IEmailService emailService)
    {
        _dbContext = sqlDatabaseContext;
        _configuration = configuration;
        _roleManager = roleManager;
        _signInManager = signinManager;
        _userManager = userManager;
        _emailService = emailService;
    }

    public string GenerateJwtToken(UserInfo user)
    {
        List<Claim> claims = new List<Claim>
        {
            new(ClaimTypes.Email,user.Id.ToString()),
            new(ClaimTypes.Name, user.Email),
            // Add additional claims for roles
        };
        foreach (string role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        string JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new Exception("No JWT configuration");
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Appsettings:LoginAuthTokenExpiryTimeInHrs"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> AddUserAsync(RegisterRequest request)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
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

            IdentityApplicationUser identityApplicationUser = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false) ?? throw new Exception("Error finding user");

            await AddUserRoleAsync(request.Email, 0).ConfigureAwait(false);

            ApplicationUser applicationUser = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber,
                IdentityApplicationUserId = identityApplicationUser.Id
            };

            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<ApplicationUser> res = await _dbContext.ApplicationUsers.AddAsync(applicationUser).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            UserInfo userInfo = new(identityApplicationUser.Id, request.Email, (await _userManager.GetRolesAsync(identityApplicationUser).ConfigureAwait(false)).ToList());

            // If everything succeeds, commit the transaction
            scope.Complete();
            return GenerateJwtToken(userInfo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            scope.Dispose(); // Ensure to dispose the TransactionScope
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User> users = (await _dbContext.ApplicationUsers
                                                .Where(u => u.Status == Status.ACTIVE)
                                                .ToListAsync().ConfigureAwait(false))
                                                .Select(user => new User
                                                {
                                                    Id = user.Id,
                                                    FirstName = user.FirstName,
                                                    LastName = user.LastName,
                                                    Country = user.Country,
                                                    PhoneNumber = user.PhoneNumber
                                                }).ToList();
        return users;
    }

    public async Task<User?> GetUserByIdAsync(int Id)
    {
        ApplicationUser? user = await _dbContext.ApplicationUsers
                                                    .Where(u => u.Id == Id && u.Status == Status.ACTIVE)
                                                    .FirstOrDefaultAsync()
                                                    .ConfigureAwait(false);
        return user;
    }

    public async Task<string> LoginUserAsync(LoginRequest request)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false).ConfigureAwait(false);

        if (!signInResult.Succeeded)
        {
            throw new Exception("Invalid Email or Password");
        }
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false) ?? throw new Exception("User does not exist");
        UserInfo userInfo = new(user.Id, request.Email, (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList());
        return GenerateJwtToken(userInfo);
    }

    public async Task<string> ForgotPasswordAsync(string email)
    {
        try
        {
            IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Invalid Email");
            string token = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
            await _emailService.SendForgetPasswordEmailAsync(email, token).ConfigureAwait(false);

            return "Success";
        }
        catch (Exception ex)
        {
            return $"Error in ForgotPasswordAsync: {ex}";
        }
    }

    public async Task<string> ResetPasswordAsync(string email, string token, string newPassword)
    {
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Invalid Email");
        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw new Exception("Invalid token");
        }

        UserInfo userInfo = new(user.Id, email, (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList());
        return GenerateJwtToken(userInfo);
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


    public async Task<bool> DeleteUserAsync(int userId)
    {
        ApplicationUser? user = await _dbContext.ApplicationUsers.FindAsync(userId).ConfigureAwait(false);

        if (user == null)
        {
            return false; // User not found
        }

        user.Status = Status.INACTIVE;

        // Update the entity state to Modified
        _dbContext.Entry(user).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return true;
    }
}