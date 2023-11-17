namespace DotnetBoilerPlate.DAL.Models;

public partial class PrintDetailsDBO
{
    public string? PrintDetailId { get; set; }

    public string? QuestionPaperId { get; set; }

    public bool? IsOnline { get; set; }

    public string? QuestionPaperPath { get; set; }

    public string? CourseId { get; set; }

    public string? ExamId { get; set; }

    public string? Instructions { get; set; }

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
