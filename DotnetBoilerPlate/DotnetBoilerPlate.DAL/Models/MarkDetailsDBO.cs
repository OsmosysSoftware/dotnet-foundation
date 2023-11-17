namespace DotnetBoilerPlate.DAL.Models;

public partial class MarkdetailsDBO
{
    public string? Markdetailsid { get; set; }

    public string? NumberOfQuestions { get; set; }

    public int ActualMarks { get; set; }

    public float? MarksSecured { get; set; }

    public float? NegativeMarks { get; set; }

    /// <summary>
    /// References marks.marksid
    /// </summary>
    public string? MarksId { get; set; }

    public string? SectionId { get; set; }

    public string? SubsectionId { get; set; }

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
