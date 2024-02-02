using DotnetFoundation.Application.DTO.AuthenticationDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Domain.Entities;
using System.Security.Claims;

namespace DotnetFoundation.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }



    public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
    {
        string res = await _userRepository.LoginUserAsync(request);
        return new(Token: res);
    }

    public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest request)
    {
        string res = await _userRepository.AddUserAsync(request);
        return new(Token: res);

    }
    public async Task<string> ForgotPasswordAsync(string email)
    {
        string res = await _userRepository.ForgotPasswordAsync(email);
        return res;
    }

    public async Task<AuthenticationResponse> ResetPasswordAsync(PasswordResetRequest request)
    {
        string res = await _userRepository.ResetPasswordAsync(request.Email, request.Token, request.Password);
        return new(Token: res);
    }

}