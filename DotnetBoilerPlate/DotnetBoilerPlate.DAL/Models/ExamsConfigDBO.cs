namespace DotnetBoilerPlate.DAL.Models;

public class ExamsConfigDBO
{
    public sbyte ConfigType { get; set; }

    public sbyte? ExamOrder { get; set; }

    public string ExamName { get; set; } = null!;

    public sbyte? ExamDuration { get; set; }

    public sbyte? ExamCutoffMarks { get; set; }

    public sbyte? IsOmr { get; set; }

    public string? SectionDetails { get; set; }

    public string? SubsectionDetails { get; set; }

    public string? PrecedingExamName { get; set; }

    /// <summary>
    /// 1-Active 2-Inactive 3-Deleted
    /// </summary>
    public bool? Status { get; set; }
}
