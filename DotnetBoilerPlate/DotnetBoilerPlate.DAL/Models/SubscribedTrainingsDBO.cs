namespace DotnetBoilerPlate.DAL.Models;

public partial class SubscribedTrainingsDBO
{
    public string TrainingId { get; set; } = null!;

    public string? UserId { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? ResString1 { get; set; }

    public string? ResString2 { get; set; }

    public int? ResInt1 { get; set; }

    public int? ResInt2 { get; set; }

    public float? ResFloat1 { get; set; }

    public float? ResFloat2 { get; set; }

    /// <summary>
    /// services id for course
    /// </summary>
    public string? CourseId { get; set; }

    public string? BatchId { get; set; }

    public float? Amount { get; set; }

    public string? TrainingProviderId { get; set; }
}
