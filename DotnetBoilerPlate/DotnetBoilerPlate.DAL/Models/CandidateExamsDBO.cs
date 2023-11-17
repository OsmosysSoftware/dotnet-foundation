namespace DotnetBoilerPlate.DAL.Models;

public class CandidateExamsDBO
{
    public string CandidateExamId { get; set; } = null!;

    public string? ExamId { get; set; }

    public string? UserId { get; set; }

    public string? ExamName { get; set; }

    public string? UserName { get; set; }

    public string? ExamUniqCode { get; set; }

    public string? QuestionPaperId { get; set; }

    public bool? IsVisible { get; set; }

    public string? TrainingProviderId { get; set; }

    /// <summary>
    /// 1 - Exam running, 2 - Exam Completed, 3 - Deative
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
