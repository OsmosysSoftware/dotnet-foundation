using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Exceptions;

public class InvalidResetPasswordTokenException : Exception
{
    public InvalidResetPasswordTokenException()
    {
    }
    public InvalidResetPasswordTokenException(string message) : base(message)
    {
    }

    public InvalidResetPasswordTokenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}