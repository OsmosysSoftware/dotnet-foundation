using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;

namespace DotnetFoundation.Services.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;

    public EmailService(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }
    public async Task<string> SendEmail(string emailId, string body)
    {
        try
        {
            string res = await _emailRepository.SendForgetPasswordEmailAsync(emailId, body).ConfigureAwait(false);
            return res;
        }
        catch (Exception ex)
        {
            return "An error occurred while sending the email: " + ex.Message;
        }
    }
}