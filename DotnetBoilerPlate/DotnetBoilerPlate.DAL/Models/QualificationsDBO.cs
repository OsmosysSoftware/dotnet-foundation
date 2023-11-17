namespace DotnetBoilerPlate.DAL.Models;

public partial class QualificationsDBO
{
    public string QualificationId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? QualificationName { get; set; }

    public string? Specialization { get; set; }

    /// <summary>
    /// 1 - School 2 - Intermediate 3 - Graduation 4 - Post Graduation
    /// </summary>
    public bool? QualificationLevel { get; set; }

    public string? SchoolCollageName { get; set; }

    /// <summary>
    /// 1-passed 2- failed 3- discontinued
    /// </summary>
    public int? CompleteStatus { get; set; }

    public int? TotalMarks { get; set; }

    public int? MarksSecured { get; set; }

    public string? BoardUniversity { get; set; }

    public float? Percentage { get; set; }

    public float? Aggregate1Year { get; set; }

    public float? Aggregate2Year { get; set; }

    public float? Aggregate3Year { get; set; }

    public float? Aggregate4Year { get; set; }

    public int? NumOfBacklogs1Year { get; set; }

    public int? NumOfBacklogs2Year { get; set; }

    public int? NumOfBacklogs3Year { get; set; }

    public int? NumOfBacklogs4Year { get; set; }

    public int? NumOfBacklogsPending1Year { get; set; }

    public int? NumOfBacklogsPending2Year { get; set; }

    public int? NumOfBacklogsPending3Year { get; set; }

    public int? NumOfBacklogsPending4Year { get; set; }

    public string TrainingProviderId { get; set; } = null!;

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? Street1 { get; set; }

    public string? Street2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? Pincode { get; set; }

    public string? ResString1 { get; set; }

    public string? ResString2 { get; set; }

    public float? ResFloat1 { get; set; }

    public float? ResFloat2 { get; set; }

    public int? ResInt1 { get; set; }

    public int? ResInt2 { get; set; }

    public short? PassedOn { get; set; }
}
