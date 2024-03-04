namespace DotnetFoundation.Application.Exceptions;


// Define a custom exception using this interface
public class IdentityUserException : Exception
{
    public IdentityUserException()
    {
    }

    public IdentityUserException(string message) : base(message)
    {
    }

    public IdentityUserException(string message, Exception innerException) : base(message, innerException)
    {
    }


}
