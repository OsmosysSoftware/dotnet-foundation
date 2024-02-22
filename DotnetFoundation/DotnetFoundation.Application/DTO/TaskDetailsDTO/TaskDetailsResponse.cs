using DotnetFoundation.Domain.Entities;
namespace DotnetFoundation.Application.DTO.TaskDetailsDTO;

public record TaskDetailsResponse(
    // TaskDetails taskDetailsRecord
    int Id,
    string Description,
    int BudgetedHours,
    int AssignedTo,
    string? Category,
    StatusEnum Status,
    DateTime CreatedOn,
    int CreatedBy,
    DateTime ModifiedOn,
    int ModifiedBy
);