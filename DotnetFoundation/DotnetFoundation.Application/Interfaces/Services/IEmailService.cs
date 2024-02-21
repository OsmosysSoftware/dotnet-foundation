using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Interfaces.Services;

/// <summary>
/// Provides functionality to send emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email to the specified email address.
    /// </summary>
    /// <param name="emailId">The email address to send the email to.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A task that represents the asynchronous send operation. The task result contains the send result.</returns>
    public Task<string> SendEmail(string emailId, string body);
}