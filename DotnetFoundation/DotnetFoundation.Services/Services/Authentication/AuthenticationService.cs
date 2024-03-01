using System.Security.Claims;
using System.Transactions;
using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;


namespace DotnetFoundation.Services.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtService;
    private readonly IEmailService _emailService;

    public AuthenticationService(IUserRepository userRepository, IJwtTokenService jwtService, IEmailService emailService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _emailService = emailService;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
    {
        UserInfo userInfo = await _userRepository.LoginUserAsync(request).ConfigureAwait(false);
        return new AuthenticationResponse
        {
            Token = _jwtService.GenerateJwtToken(userInfo),
        };
    }

    public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest request)
    {
        using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

        string userId = await _userRepository.AddUserAsync(request).ConfigureAwait(false);
        await _userRepository.AddUserRoleAsync(request.Email, 0).ConfigureAwait(false);

        List<string> userRoles = await _userRepository.GetUserRoleAsync(request.Email).ConfigureAwait(false);

        UserInfo userInfo = new(userId, request.Email, userRoles);

        // If everything succeeds, commit the transaction
        scope.Complete();

        return new AuthenticationResponse
        {
            Token = _jwtService.GenerateJwtToken(userInfo),
        };
    }

    public async Task ForgotPasswordAsync(string email)
    {
        string token = await _userRepository.ForgotPasswordAsync(email).ConfigureAwait(false);
        await _emailService.SendForgetPasswordEmailAsync(email, token).ConfigureAwait(false);
    }

    public async Task ResetPasswordAsync(PasswordResetRequest request)
    {
        await _userRepository.ResetPasswordAsync(request.Email, request.Token, request.Password).ConfigureAwait(false);
    }
}