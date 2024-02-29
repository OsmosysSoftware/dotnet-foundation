namespace DotnetFoundation.Application.Exceptions;


// Define a custom exception using this interface
public class IdentityUserCreationException : Exception
{
    public IdentityUserCreationException()
    {
    }

    public IdentityUserCreationException(string message) : base(message)
    {
    }

    public IdentityUserCreationException(string message, Exception innerException) : base(message, innerException)
    {
    }


}
