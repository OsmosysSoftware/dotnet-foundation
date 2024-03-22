namespace DotnetFoundation.Application.Interfaces.Validator;

public interface IUserValidator
{
    public Task<bool> ValidUserId(int userId);
    public Task<bool> ValidEmailId(string email);
    public Task<bool> IsEmailRegistered(string email);

}