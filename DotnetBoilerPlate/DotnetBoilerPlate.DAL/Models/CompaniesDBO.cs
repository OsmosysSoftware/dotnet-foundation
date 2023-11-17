namespace DotnetBoilerPlate.DAL.Models;

public class CompaniesDBO
{
    public string CompanyId { get; set; } = null!;

    /// <summary>
    /// 0-Training center, 1=Campus Placement, 2-for main question bank
    /// </summary>
    public int? CompanyType { get; set; }

    public string? UniqueCode { get; set; }

    public string? TrainingProviderId { get; set; }

    public string? CompanyName { get; set; }

    public string? ContactName { get; set; }

    public string? ContactNumber { get; set; }

    public string? CompanyUrl { get; set; }

    public string? LinkedIn { get; set; }

    public string? Facebook { get; set; }

    /// <summary>
    /// Path of company Logo
    /// </summary>
    public string? CompanyLogo { get; set; }

    public string? CompanyAddress { get; set; }

    /// <summary>
    /// references themetemplates.themetemplateid
    /// </summary>
    public sbyte? ThemeId { get; set; }

    /// <summary>
    /// 0 - Don&apos;t share db, 1 - share db
    /// </summary>
    public sbyte? IsShareDb { get; set; }

    public DateOnly? ExamDate { get; set; }

    public TimeOnly? ExamTime { get; set; }

    public string? Comments { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
