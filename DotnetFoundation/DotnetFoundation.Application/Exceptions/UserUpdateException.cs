using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Exceptions;

public class UserUpdateException : Exception
{
    public UserUpdateException()
    {
    }

    public UserUpdateException(string message) : base(message)
    {
    }

    public UserUpdateException(string message, Exception innerException) : base(message, innerException)
    {
    }
}