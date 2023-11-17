namespace DotnetBoilerPlate.DAL.Models;

public class CoursebatchesDBO
{
    public string CourseBatchId { get; set; } = null!;

    public string? CourseId { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? StartTime { get; set; }

    public float? Duration { get; set; }

    /// <summary>
    /// employeeid
    /// </summary>
    public string? Coordinator { get; set; }

    public int? MaxNumber { get; set; }

    public int? MinNumber { get; set; }

    public string? TrainingBranch { get; set; }

    public string? ResString1 { get; set; }

    public string? ResString2 { get; set; }

    public float? ResFloat1 { get; set; }

    public float? ResFloat2 { get; set; }

    public int? ResInt1 { get; set; }

    public int? ResInt2 { get; set; }

    public float? Amount { get; set; }

    public float? LabHours { get; set; }

    public float? ClassHours { get; set; }

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
