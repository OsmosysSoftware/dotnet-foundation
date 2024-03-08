using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Exceptions;

public class InvalidTokenException : Exception
{
    public InvalidTokenException()
    {
    }
    public InvalidTokenException(string message) : base(message)
    {
    }

    public InvalidTokenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}