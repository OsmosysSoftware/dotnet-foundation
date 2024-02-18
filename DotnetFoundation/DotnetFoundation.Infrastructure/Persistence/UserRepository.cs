using DotnetFoundation.Application.DTO.AuthenticationDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Services.EmailService;
using DotnetFoundation.Domain.Entities;
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
    public UserRepository(IConfiguration configuration, SqlDatabaseContext sqlDatabaseContext, SignInManager<IdentityApplicationUser> signinManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityApplicationUser> userManager)
    {
        _dbContext = sqlDatabaseContext;
        _configuration = configuration;
        _roleManager = roleManager;
        _signInManager = signinManager;
        _userManager = userManager;
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
        string JWT_KEY = _configuration["Jwt:Key"] ?? throw new Exception("No JWT configuration");
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires: DateTime.Now.AddHours(2),
        signingCredentials: credentials
    );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> AddUserAsync(RegisterRequest request)
    {
        TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

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
        List<User> users = (await _dbContext.ApplicationUsers.ToListAsync().ConfigureAwait(false)).Select(user => new User { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName }).ToList();
        return users;


    }

    public async Task<User?> GetUserByIdAsync(int Id)
    {
        ApplicationUser? user = await _dbContext.ApplicationUsers.FindAsync(Id).ConfigureAwait(false);
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
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Invalid Email");
        if (user != null)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
            EmailService emailService = new EmailService();
            await emailService.SendEmail(email, "forget password", token).ConfigureAwait(false);
        }
        return "ok";
    }

    public async Task<string> ResetPasswordAsync(string email, string token, string newPassword)
    {
        IdentityApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false) ?? throw new Exception("Invalid Email");
        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
        if (!result.Succeeded) throw new Exception("Invalid token");
        UserInfo userInfo = new(user.Id, email, (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList());
        return GenerateJwtToken(userInfo);
    }

    public async Task<bool> AddUserRoleAsync(string email, int role)
    {
        string newRole = ((Roles)role).ToString();
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