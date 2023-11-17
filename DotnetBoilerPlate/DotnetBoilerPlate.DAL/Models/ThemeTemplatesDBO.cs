namespace DotnetBoilerPlate.DAL.Models;

public partial class ThemeTemplatesDBO
{
    public sbyte ThemeTemplateId { get; set; }

    public string? ThemeTemplateName { get; set; }

    public string? ThemeTemplatePath { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
