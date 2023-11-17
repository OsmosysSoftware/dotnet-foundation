namespace DotnetBoilerPlate.DAL.Models;

public partial class SectionsDBO
{
    public string SectionId { get; set; } = null!;

    public string? SectionDescription { get; set; }

    public string Suggestions { get; set; } = null!;

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
