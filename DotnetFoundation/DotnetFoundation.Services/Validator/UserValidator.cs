using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Validator;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Services.Validator;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> IsEmailRegistered(string email)
    {
        return await _userRepository.CheckEmailExist(email).ConfigureAwait(false);
    }
    public async Task<bool> ValidUserId(int userId)
    {
        User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
        return user != null;
    }
    public async Task<bool> ValidEmailId(string email)
    {
        return await _userRepository.CheckEmailRegistered(email).ConfigureAwait(false);
    }

}