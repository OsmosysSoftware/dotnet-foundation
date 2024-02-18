using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Interfaces.Persistence;

public interface IEmailRepository
{
    public Task<string> SendForgetPasswordEmailAsync(string email, string subject, string body);

}