namespace DotnetBoilerPlate.DAL.Models;

public class GroupsDBO
{
    public string GroupId { get; set; } = null!;

    public int GroupDisplayId { get; set; }

    public string? ImagePath { get; set; }

    /// <summary>
    /// Passage for comprehension questions
    /// </summary>
    public string? GroupDescription { get; set; }

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
