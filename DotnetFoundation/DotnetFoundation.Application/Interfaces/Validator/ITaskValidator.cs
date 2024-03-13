namespace DotnetFoundation.Application.Interfaces.Validator;

public interface ITaskValidator
{
    public Task<bool> ValidTaskId(int taskId);
    public Task<bool> ValidAssignedTo(int userId);
}