namespace DotnetBoilerPlate.DAL.Models;

public class LogsDBO
{
    public string LogId { get; set; } = null!;

    public string? UserName { get; set; }

    public sbyte? UserType { get; set; }

    public string? EmailAddress { get; set; }

    public string? PageName { get; set; }

    public string Action { get; set; } = null!;

    public string? TrainingProviderId { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
