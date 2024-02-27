namespace DotnetFoundation.Application.Interfaces.Integrations;

/// <summary>
/// Provides functionality to send emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email to the specified email address.
    /// </summary>
    /// <param name="email">The email address to send the email to.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A task that represents the asynchronous send operation. The task result contains the send result.</returns>
    public Task<string> SendForgetPasswordEmailAsync(string email, string body);
}
