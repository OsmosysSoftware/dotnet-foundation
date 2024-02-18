using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;

namespace DotnetFoundation.Application.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;

    public EmailService()
    {
    }

    public EmailService(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }
    public async Task<string> SendEmail(string emailId, string subject, string body)
    {
        string res = await _emailRepository.SendForgetPasswordEmailAsync(emailId, subject, body).ConfigureAwait(false);
        return res;
    }
}