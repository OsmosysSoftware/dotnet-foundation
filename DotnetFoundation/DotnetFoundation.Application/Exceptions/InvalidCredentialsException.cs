using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
    {
    }

    public InvalidCredentialsException(string message) : base(message)
    {
    }

    public InvalidCredentialsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}