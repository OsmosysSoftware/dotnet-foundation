namespace DotnetBoilerPlate.DAL.Models;

public class GetCandidateExamsDBO
{
    public string CandidateExamId { get; set; } = null!;

    public string? ExamId { get; set; }

    public string? UserId { get; set; }

    public string? ExamUniqCode { get; set; }

    /// <summary>
    /// 1 - Exam running, 2 - Exam Completed, 3 - Deative
    /// </summary>
    public bool? Status { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public string? UserName { get; set; }

    public string? ExamsExamId { get; set; }

    public string? ExamName { get; set; }

    /// <summary>
    /// 1-Free Exam 2-Paid Exam 3- Course/Batch Exam
    /// </summary>
    public short? ExamType { get; set; }

    public string? BatchId { get; set; }

    public DateOnly? ExamDate { get; set; }

    public DateTime? ExamsCreatedOn { get; set; }

    public DateTime? ExamsModifiedOn { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted
    /// </summary>
    public bool? ExamsStatus { get; set; }

    public string? ExamsReatedBy { get; set; }

    public string? ExamsModifiedBy { get; set; }

    public TimeOnly? ExamTime { get; set; }

    public string? ExamsExamUniqCode { get; set; }

    public bool? IsCourseExam { get; set; }

    public string? UsersUserId { get; set; }

    public string? UserDisplayId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Salutation { get; set; }

    public string? EmailAddress { get; set; }

    public string? Password { get; set; }

    /// <summary>
    /// 0-Admin, 1-employee, 2-trainee, 3-super user, 4-trainingprovider
    /// </summary>
    public int? UserType { get; set; }

    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// 1 - active, 2 - deactive, 3 - deleted, 0 - dummy user, Shouldnot use 9
    /// </summary>
    public bool? UsersStatus { get; set; }

    public DateTime? UsersCreatedOn { get; set; }

    public DateTime? UsersModifiedOn { get; set; }

    public string? UsersCreatedBy { get; set; }

    public string? UsersModifiedBy { get; set; }

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

    /// <summary>
    /// Path of thumb impression
    /// </summary>
    public string? ThumbImpression { get; set; }

    /// <summary>
    /// Path of trainee photo
    /// </summary>
    public string? TraineePhoto { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmergencyContactNumber { get; set; }

    public string? LinkedinId { get; set; }

    public string? FacebookId { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? TrainingProviderId { get; set; }

    public bool? Gender { get; set; }
}
