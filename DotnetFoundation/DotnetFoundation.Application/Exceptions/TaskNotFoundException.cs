namespace DotnetFoundation.Application.Exceptions;

public class TaskNotFoundException : NotFoundException
{
    public TaskNotFoundException(string message) : base(message)
    {
    }

    public TaskNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public TaskNotFoundException()
    {
    }
}