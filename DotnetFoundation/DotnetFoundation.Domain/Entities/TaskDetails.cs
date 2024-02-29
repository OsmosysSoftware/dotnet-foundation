using DotnetFoundation.Domain.Common;

namespace DotnetFoundation.Domain.Entities;

public class TaskDetails : BaseEntity
{
    public string Description { get; set; } = null!;
    public int BudgetedHours { get; set; }
    public int AssignedTo { get; set; }
    public string? Category { get; set; }
}