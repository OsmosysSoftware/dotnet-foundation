using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Interfaces.Persistence;

/// <summary>
/// Represents the repository interface for handling email operations.
/// </summary>
public interface IEmailRepository
{
    /// <summary>
    /// Sends a forget password email to the specified email address.
    /// </summary>
    /// <param name="email">The email address to send the forget password email to.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A task that represents the asynchronous send operation. The task result contains the email sending status.</returns>
    public Task<string> SendForgetPasswordEmailAsync(string email, string body);

}