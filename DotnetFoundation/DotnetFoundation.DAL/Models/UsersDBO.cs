namespace DotnetFoundation.DAL.Models;

public partial class UsersDBO
{
    public string UserId { get; set; } = null!;

    public string? UserDisplayId { get; set; }

    /// <summary>
    /// 0-Admin, 1-employee, 2-trainee, 3-super user, 4-trainingprovider
    /// </summary>
    public int? UserType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Salutation { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmergencyContactNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Password { get; set; }

    public string? HallTicket { get; set; }

    public sbyte? QuestionPaperSetNumber { get; set; }

    public string? LinkedinId { get; set; }

    public string? FacebookId { get; set; }

    public bool? IsAcceptedTermsAndConditions { get; set; }

    public sbyte UnSubscribe { get; set; }

    /// <summary>
    /// Path of profile picture
    /// </summary>
    public string? ProfilePicPath { get; set; }

    /// <summary>
    /// Path of trainee photo
    /// </summary>
    public string? TraineePhoto { get; set; }

    /// <summary>
    /// Path of finger print
    /// </summary>
    public string? FingerPrintValue { get; set; }

    /// <summary>
    /// Path of thumb impression
    /// </summary>
    public string? ThumbImpression { get; set; }

    public bool? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Comments { get; set; }

    public string? CandidateComments { get; set; }

    public string? TrainingProviderId { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted, 0 - dummy user, Shouldnot use 9
    /// </summary>
    public bool? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? Address1Street1 { get; set; }

    public string? Address1Street2 { get; set; }

    public string? Address1City { get; set; }

    public string? Address1State { get; set; }

    public string? Address1Country { get; set; }

    public string? Address1Pincode { get; set; }

    public string? Address2Street1 { get; set; }

    public string? Address2Street2 { get; set; }

    public string? Address2City { get; set; }

    public string? Address2State { get; set; }

    public string? Address2Country { get; set; }

    public string? Address2Pincode { get; set; }

    public string? ResString1 { get; set; }

    public string? ResString2 { get; set; }

    public float? ResFloat1 { get; set; }

    public float? ResFloat2 { get; set; }

    public int? ResInt1 { get; set; }

    public int? ResInt2 { get; set; }

    public string? Features { get; set; }
}
