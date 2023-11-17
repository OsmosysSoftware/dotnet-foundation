namespace DotnetBoilerPlate.DAL.Models;

public partial class QuestiondetailsDBO
{
    public string? QuestionDetailId { get; set; }

    public string? QuestionPaperId { get; set; }

    public string? ExamId { get; set; }

    public string? NumberOfQuestions { get; set; }

    public string? MarksExamined { get; set; }

    public string? CutOffMarks { get; set; }

    public sbyte? IsOmr { get; set; }

    public sbyte? IsOnline { get; set; }

    public int? GroupCount { get; set; }

    public float? NegativeMarks { get; set; }

    public string? CourseId { get; set; }

    public string? SectionId { get; set; }

    public string? SubsectionId { get; set; }

    public string TrainingProviderId { get; set; } = null!;

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
