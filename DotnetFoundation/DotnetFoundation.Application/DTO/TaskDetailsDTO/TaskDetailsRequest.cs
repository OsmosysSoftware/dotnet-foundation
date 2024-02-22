namespace DotnetFoundation.Application.DTO.TaskDetailsDTO;

public record TaskDetailsRequest(
    string Description,
    int BudgetedHours,
    int AssignedTo,
    string? Category
);