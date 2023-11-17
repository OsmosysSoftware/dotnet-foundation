namespace DotnetBoilerPlate.DAL.Models;

public partial class ServicesDBO
{
    public string ServiceId { get; set; } = null!;

    public string? ServiceName { get; set; }

    /// <summary>
    /// 1- single course 2- multiple coureses
    /// </summary>
    public short? ServiceType { get; set; }

    /// <summary>
    /// stores multiple serviceids seperated with comma
    /// </summary>
    public string? ChildServiceId { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public short? MaxNumber { get; set; }

    public short? MinNumber { get; set; }

    public float? Fees { get; set; }

    /// <summary>
    /// employeeid
    /// </summary>
    public string? Faculty { get; set; }

    public float? CourseDuration { get; set; }

    /// <summary>
    /// 2- exam 1- course
    /// </summary>
    public bool? IsExam { get; set; }

    public bool? IsFreeExam { get; set; }

    /// <summary>
    /// 1-FreeExam 0-PaidExam
    /// </summary>
    public float? Amount { get; set; }

    public float? ClassHours { get; set; }

    public float? LabHours { get; set; }

    public string TrainingProviderId { get; set; } = null!;

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

    public float? ResFloat1 { get; set; }

    public float? ResFloat2 { get; set; }

    public int? ResInt1 { get; set; }

    public int? ResInt2 { get; set; }
}
