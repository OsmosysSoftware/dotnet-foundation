namespace DotnetBoilerPlate.DAL.Models;

public partial class QuestionsDBO
{
    public string QuestionId { get; set; } = null!;

    /// <summary>
    /// For comprehension questions, Actual passage is in groups(groupdescription) table and references groups(groupid) 
    /// </summary>
    public string? GroupId { get; set; }

    /// <summary>
    /// 1 - the questionis of OMR type, 2- the questionis non OMR type
    /// </summary>
    public bool? IsOmr { get; set; }

    public bool Complexity { get; set; }

    public string? QuestionDescription { get; set; }

    public string? QuestionHint { get; set; }

    public string? Option1 { get; set; }

    public string? Option2 { get; set; }

    public string? Option3 { get; set; }

    public string? Option4 { get; set; }

    /// <summary>
    /// It contains one of four opions i.e a,b,c,d
    /// </summary>
    public string? Answer { get; set; }

    public string? AnswerFormat { get; set; }

    public string? DescriptiveAnswer { get; set; }

    public string? QuestionFilePath { get; set; }

    public string? Option1FilePath { get; set; }

    public string? Option2FilePath { get; set; }

    public string? Option3FilePath { get; set; }

    public string? Option4FilePath { get; set; }

    public string? AnswerFilePath { get; set; }

    public string? DescriptiveAnswerExplanation { get; set; }

    public string? ExamorCourseId { get; set; }

    public string SectionId { get; set; } = null!;

    public string SubsectionId { get; set; } = null!;

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
