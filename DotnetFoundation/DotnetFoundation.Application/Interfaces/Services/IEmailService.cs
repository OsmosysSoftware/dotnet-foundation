using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Interfaces.Services;

public interface IEmailService
{
    public Task<string> SendEmail(string emailId, string subject, string body);
}