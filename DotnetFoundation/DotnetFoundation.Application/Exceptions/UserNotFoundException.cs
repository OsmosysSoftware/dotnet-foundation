namespace DotnetFoundation.Application.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public UserNotFoundException()
    {
    }
}