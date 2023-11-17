namespace DotnetBoilerPlate.DAL.Models;

public class ExamsDBO
{
    public string ExamId { get; set; } = null!;

    public string? ExamName { get; set; }

    /// <summary>
    /// 1-Free Exam 2-Paid Exam 3- Course/Batch Exam
    /// </summary>
    public short? ExamType { get; set; }

    public DateOnly? ExamDate { get; set; }

    public TimeOnly? ExamTime { get; set; }

    public string? ExamDuration { get; set; }

    public float? TotalMarksExamined { get; set; }

    public string? TotalCutOffMarks { get; set; }

    /// <summary>
    /// List of examids that are to be cleared to write this exam
    /// </summary>
    public string? PrecedingExamIds { get; set; }

    public bool? IsCourseExam { get; set; }

    public bool? IsOnline { get; set; }

    public bool? IsVisible { get; set; }

    public bool? ShowHint { get; set; }

    public bool? ShowDescription { get; set; }

    /// <summary>
    /// Using one questionpaperid, Provision for 5 comma separated questionpaperids ex:OQN-20151029024904
    /// </summary>
    public string? QuestionPaperIds { get; set; }

    public string? QuestionPaperId { get; set; }

    public string? ExamUniqCode { get; set; }

    public string? BatchId { get; set; }

    public string? ServiceId { get; set; }

    public string? TrainingProviderId { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? SubTopic1 { get; set; }

    public string? SubTopic2 { get; set; }

    public string? SubTopic3 { get; set; }

    public string? SubTopic4 { get; set; }

    public string? SubTopic5 { get; set; }

    public string? SubTopic6 { get; set; }

    public string? ResString1 { get; set; }

    public string? ResString2 { get; set; }

    public float? ResFloat1 { get; set; }

    public float? ResFloat2 { get; set; }

    public int? ResInt1 { get; set; }

    public int? ResInt2 { get; set; }
}
