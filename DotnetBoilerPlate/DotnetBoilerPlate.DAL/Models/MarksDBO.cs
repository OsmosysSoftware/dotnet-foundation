namespace DotnetBoilerPlate.DAL.Models;

public partial class MarksDBO
{
    public string MarksId { get; set; } = null!;

    public string? ExamId { get; set; }

    public string? UserId { get; set; }

    public string? ExamWrittenDate { get; set; }

    public float? TotalMarksExamined { get; set; }

    public string? CutOffMarks { get; set; }

    public float? TotalMarksSecured { get; set; }

    public float? NegativeMarksSecured { get; set; }

    public float? PositiveMarksSecured { get; set; }

    public string? TimeRelapsed { get; set; }

    public sbyte? IsReportSent { get; set; }

    public DateTime? ReportDate { get; set; }

    public string? QuestionPaperId { get; set; }

    public string? CourseId { get; set; }

    public string? TrainingProviderId { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? Grade { get; set; }

    public string? ResString1 { get; set; }

    public string? ResString2 { get; set; }

    public int? ResInt1 { get; set; }

    public int? ResInt2 { get; set; }

    public float? ResFloat1 { get; set; }

    public float? ResFloat2 { get; set; }
}
