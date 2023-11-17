namespace DotnetBoilerPlate.DAL.Models;

public partial class OnlineExamSubmittedAnswersDBO
{
    public DateTime? CreatedOn { get; set; }

    public string UserId { get; set; } = null!;

    public string ExamId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public string? TableName { get; set; }

    public string? CorrectAnswer { get; set; }

    public string? SubmittedAnswer { get; set; }

    public int? QuestionNumber { get; set; }

    public string SectionId { get; set; } = null!;

    public string SubsectionId { get; set; } = null!;

    public TimeOnly? SubmittedTime { get; set; }

    public string? TrainingProviderId { get; set; }

    public float? PositiveMarks { get; set; }

    public float? NegativeMarks { get; set; }

    public float ObtainedMarks { get; set; }
}
