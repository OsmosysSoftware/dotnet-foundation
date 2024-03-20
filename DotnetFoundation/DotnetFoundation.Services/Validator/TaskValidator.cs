using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Validator;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Services.Validator;

public class TaskValidator : ITaskValidator
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskDetailsRepository _taskRepository;

    public TaskValidator(IUserRepository userRepository, ITaskDetailsRepository taskRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
    }
    public async Task<bool> ValidAssignedTo(int userId)
    {
        User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
        return user != null;
    }
    public async Task<bool> ValidTaskId(int taskId)
    {
        TaskDetails? task = await _taskRepository.GetTaskByIdAsync(taskId).ConfigureAwait(false);
        return task != null;
    }
}