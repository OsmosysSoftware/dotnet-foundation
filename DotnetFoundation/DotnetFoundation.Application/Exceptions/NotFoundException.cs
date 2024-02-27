namespace DotnetFoundation.Application.Exceptions;

/// <summary>
/// Represents an error when a requested resource is not found.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    public NotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specific message.
    /// </summary>
    /// <param name="msg">The message that describes the error.</param>
    public NotFoundException(string msg) : base(msg)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specific message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
