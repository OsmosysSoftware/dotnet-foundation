using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;

namespace DotnetFoundation.Application.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IUserRepository _userRepository;
    public EmailService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<string> SendEmail(string emailId, string subject, string body)
    {
        string res = await _userRepository.SendForgetPasswordEmail(emailId, subject, body).ConfigureAwait(false);
        return res;
    }
}