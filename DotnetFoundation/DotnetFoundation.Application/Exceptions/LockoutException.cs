namespace DotnetFoundation.Application.Exceptions;

public class LockoutException : Exception
{
    public LockoutException()
    {
    }

    public LockoutException(string message) : base(message)
    {
    }

    public LockoutException(string message, Exception innerException) : base(message, innerException)
    {
    }
}