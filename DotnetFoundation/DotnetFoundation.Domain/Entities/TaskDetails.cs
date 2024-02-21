namespace DotnetFoundation.Domain.Entities;

public enum StatusEnum
{
    Inactive = 0,
    Active = 1
}

public class TaskDetails
{
    public int Id;
    public string Description { get; set; } = null!;
    public int BudgetedHours { get; set; } = 0;
    public int AssignedTo { get; set; }
    public string? Category { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int ModifiedBy { get; set; }
}