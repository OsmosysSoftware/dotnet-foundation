namespace DotnetBoilerPlate.DAL.Models;

public partial class SubsectionsDBO
{
    public string SubsectionId { get; set; } = null!;

    public string? SubsectionDescription { get; set; }

    public string? SectionId { get; set; }

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
