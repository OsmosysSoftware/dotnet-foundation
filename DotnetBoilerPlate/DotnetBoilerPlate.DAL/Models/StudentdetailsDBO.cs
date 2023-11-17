namespace DotnetBoilerPlate.DAL.Models;

public partial class StudentdetailsDBO
{
    /// <summary>
    /// hall ticket number is unique for every candidate
    /// </summary>
    public string HallTicketNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Branch { get; set; }

    public float? FirstYearPercentage { get; set; }

    public float? SecondYearPercentage { get; set; }

    public float? ThirdYearPercentage { get; set; }

    public float? OverallPercentage { get; set; }

    public int? FirstYearBackLogs { get; set; }

    public int? SecondYearBackLogs { get; set; }

    public int? ThirdYearBackLogs { get; set; }

    public string? EmailId { get; set; }

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
