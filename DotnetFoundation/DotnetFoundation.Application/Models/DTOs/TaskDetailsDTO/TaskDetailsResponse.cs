using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;

/// <summary>
/// Represents the response for authentication-related actions.
/// </summary>
public record TaskDetailsResponse
{
    public int Id { get; init; }
    public string Description { get; init; }
    public int BudgetedHours { get; init; }
    public int AssignedTo { get; init; }
    public string? Category { get; init; }
    public Status Status { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime ModifiedOn { get; init; }
    public int ModifiedBy { get; init; }
}