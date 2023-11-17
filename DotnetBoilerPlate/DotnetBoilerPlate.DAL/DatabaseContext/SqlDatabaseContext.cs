using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.DAL.Models;

namespace DotnetBoilerPlate.DAL.DatabaseContext
{
    public partial class SqlDatabaseContext : DbContext
    {
        public SqlDatabaseContext()
        {
        }

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<CandidateExamsDBO> CandidateExams { get; set; }

        public DbSet<CompaniesDBO> Companies { get; set; }

        public DbSet<CoursebatchesDBO> CourseBatches { get; set; }

        public DbSet<ExamsDBO> Exams { get; set; }

        public DbSet<ExamsConfigDBO> ExamsConfigs { get; set; }

        public DbSet<GetCandidateExamsDBO> GetCandidateExams { get; set; }

        public DbSet<GroupsDBO> Groups { get; set; }

        public DbSet<LogsDBO> Logs { get; set; }

        public DbSet<MarksDBO> Marks { get; set; }

        public DbSet<MarkdetailsDBO> MarkDetails { get; set; }

        public DbSet<OnlineExamQuestionPapersDBO> OnlineExamQuestionPapers { get; set; }

        public DbSet<OnlineExamSubmittedAnswersDBO> OnlineExamSubmittedAnswers { get; set; }

        public DbSet<PrintDetailsDBO> PrintDetails { get; set; }

        public DbSet<QualificationsDBO> Qualifications { get; set; }

        public DbSet<QuestionsDBO> Questions { get; set; }

        public DbSet<QuestiondetailsDBO> QuestionDetails { get; set; }

        public DbSet<SectionsDBO> Sections { get; set; }

        public DbSet<ServicesDBO> Services { get; set; }

        public DbSet<StudentdetailsDBO> StudentDetails { get; set; }

        public DbSet<SubscribedTrainingsDBO> SubscribedTrainings { get; set; }

        public DbSet<SubsectionsDBO> Subsections { get; set; }

        public DbSet<TestUserRegistrationdetailsDBO> TestUserRegistrationDetails { get; set; }

        public DbSet<ThemeTemplatesDBO> ThemeTemplates { get; set; }

        public DbSet<TrainingprovidersDBO> TrainingProviders { get; set; }

        public DbSet<UsersDBO> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<CandidateExamsDBO>(entity =>
            {
                entity.HasKey(e => e.CandidateExamId).HasName("PRIMARY");

                entity
                    .ToTable("candidate_exam")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.TrainingProviderId, e.ExamId, e.UserId }, "idx_ce_tpid_userid_examid");

                entity.Property(e => e.CandidateExamId)
                    .HasMaxLength(36)
                    .HasColumnName("candidateexamid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasColumnName("examid");
                entity.Property(e => e.ExamName)
                    .HasMaxLength(50)
                    .HasColumnName("examname");
                entity.Property(e => e.ExamUniqCode)
                    .HasMaxLength(20)
                    .HasColumnName("examuniqcode");
                entity.Property(e => e.IsVisible).HasColumnName("isvisible");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.QuestionPaperId)
                    .HasMaxLength(36)
                    .HasColumnName("questionpaperid");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - Exam running, 2 - Exam Completed, 3 - Deative")
                    .HasColumnName("status");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("userid");
                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<CompaniesDBO>(entity =>
            {
                entity.HasKey(e => e.CompanyId).HasName("PRIMARY");

                entity
                    .ToTable("company")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(36)
                    .HasColumnName("companyid");
                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .HasColumnName("comments");
                entity.Property(e => e.CompanyAddress)
                    .HasMaxLength(100)
                    .HasColumnName("companyaddress");
                entity.Property(e => e.CompanyLogo)
                    .HasMaxLength(100)
                    .HasComment("Path of company Logo")
                    .HasColumnName("companylogo");
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(500)
                    .HasColumnName("companyname");
                entity.Property(e => e.CompanyType)
                    .HasComment("0-Training center, 1=Campus Placement, 2-for main question bank")
                    .HasColumnName("companytype");
                entity.Property(e => e.CompanyUrl)
                    .HasMaxLength(100)
                    .HasColumnName("companyurl");
                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .HasColumnName("contactname");
                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(20)
                    .HasColumnName("contactnumber");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ExamDate).HasDefaultValueSql("'2015-11-08'");
                entity.Property(e => e.ExamTime)
                    .HasDefaultValueSql("'09:30:00'")
                    .HasColumnType("time");
                entity.Property(e => e.Facebook)
                    .HasMaxLength(50)
                    .HasColumnName("facebook");
                entity.Property(e => e.IsShareDb)
                    .HasDefaultValueSql("'0'")
                    .HasComment("0 - Don't share db, 1 - share db")
                    .HasColumnName("IsShareDB");
                entity.Property(e => e.LinkedIn)
                    .HasMaxLength(50)
                    .HasColumnName("linkedin");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.ThemeId)
                    .HasDefaultValueSql("'0'")
                    .HasComment("references themetemplates.themetemplateid")
                    .HasColumnName("themeid");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UniqueCode)
                    .HasMaxLength(15)
                    .HasColumnName("uniquecode");
            });

            modelBuilder.Entity<CoursebatchesDBO>(entity =>
            {
                entity.HasKey(e => e.CourseBatchId).HasName("PRIMARY");

                entity
                    .ToTable("coursebatch")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CourseId, "FK_coursebatch");

                entity.Property(e => e.CourseBatchId)
                    .HasMaxLength(36)
                    .HasColumnName("coursebatchid");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.ClassHours).HasColumnName("classhours");
                entity.Property(e => e.Coordinator)
                    .HasMaxLength(36)
                    .HasComment("employeeid")
                    .HasColumnName("coordinator");
                entity.Property(e => e.CourseId)
                    .HasMaxLength(36)
                    .HasColumnName("courseid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.Duration).HasColumnName("duration");
                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("enddate");
                entity.Property(e => e.LabHours).HasColumnName("labhours");
                entity.Property(e => e.MaxNumber).HasColumnName("maxnumber");
                entity.Property(e => e.MinNumber).HasColumnName("minnumber");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
                entity.Property(e => e.ResFloat1).HasColumnName("res_float1");
                entity.Property(e => e.ResFloat2).HasColumnName("res_float2");
                entity.Property(e => e.ResInt1).HasColumnName("res_int1");
                entity.Property(e => e.ResInt2).HasColumnName("res_int2");
                entity.Property(e => e.ResString1)
                    .HasMaxLength(100)
                    .HasColumnName("res_string1");
                entity.Property(e => e.ResString2)
                    .HasMaxLength(100)
                    .HasColumnName("res_string2");
                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startdate");
                entity.Property(e => e.StartTime)
                    .HasMaxLength(8)
                    .HasColumnName("starttime");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.TrainingBranch)
                    .HasMaxLength(100)
                    .HasColumnName("trainingbranch");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<ExamsDBO>(entity =>
            {
                entity.HasKey(e => e.ExamId).HasName("PRIMARY");

                entity
                    .ToTable("exams")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BatchId, "FK_exams");

                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasColumnName("examid");
                entity.Property(e => e.BatchId)
                    .HasMaxLength(36)
                    .HasColumnName("batchid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ExamDate).HasColumnName("examdate");
                entity.Property(e => e.ExamDuration)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'0'")
                    .HasColumnName("examduration");
                entity.Property(e => e.ExamName)
                    .HasMaxLength(100)
                    .HasColumnName("examname");
                entity.Property(e => e.ExamTime)
                    .HasColumnType("time")
                    .HasColumnName("examtime");
                entity.Property(e => e.ExamType)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1-Free Exam 2-Paid Exam 3- Course/Batch Exam")
                    .HasColumnName("examtype");
                entity.Property(e => e.ExamUniqCode)
                    .HasMaxLength(20)
                    .HasColumnName("examuniqcode");
                entity.Property(e => e.IsCourseExam)
                    .HasDefaultValueSql("'1'")
                    .HasColumnName("iscourseexam");
                entity.Property(e => e.IsOnline)
                    .HasDefaultValueSql("'0'")
                    .HasColumnName("isonline");
                entity.Property(e => e.IsVisible).HasColumnName("isvisible");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.PrecedingExamIds)
                    .HasMaxLength(400)
                    .HasComment("List of examids that are to be cleared to write this exam")
                    .HasColumnName("precedingexamids");
                entity.Property(e => e.QuestionPaperId)
                    .HasMaxLength(36)
                    .HasColumnName("questionpaperid");
                entity.Property(e => e.QuestionPaperIds)
                    .HasMaxLength(100)
                    .HasComment("Using one questionpaperid, Provision for 5 comma separated questionpaperids ex:OQN-20151029024904")
                    .HasColumnName("questionpaperids");
                entity.Property(e => e.ResFloat1).HasColumnName("res_float1");
                entity.Property(e => e.ResFloat2).HasColumnName("res_float2");
                entity.Property(e => e.ResInt1).HasColumnName("res_int1");
                entity.Property(e => e.ResInt2).HasColumnName("res_int2");
                entity.Property(e => e.ResString1)
                    .HasMaxLength(100)
                    .HasColumnName("res_string1");
                entity.Property(e => e.ResString2)
                    .HasMaxLength(100)
                    .HasColumnName("res_string2");
                entity.Property(e => e.ServiceId)
                    .HasMaxLength(36)
                    .HasColumnName("serviceid");
                entity.Property(e => e.ShowDescription)
                    .HasDefaultValueSql("'0'")
                    .HasColumnName("showdescription");
                entity.Property(e => e.ShowHint)
                    .HasDefaultValueSql("'0'")
                    .HasColumnName("showhint");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.SubTopic1)
                    .HasMaxLength(20)
                    .HasColumnName("subtopic1");
                entity.Property(e => e.SubTopic2)
                    .HasMaxLength(20)
                    .HasColumnName("subtopic2");
                entity.Property(e => e.SubTopic3)
                    .HasMaxLength(20)
                    .HasColumnName("subtopic3");
                entity.Property(e => e.SubTopic4)
                    .HasMaxLength(20)
                    .HasColumnName("subtopic4");
                entity.Property(e => e.SubTopic5)
                    .HasMaxLength(20)
                    .HasColumnName("subtopic5");
                entity.Property(e => e.SubTopic6)
                    .HasMaxLength(20)
                    .HasColumnName("subtopic6");
                entity.Property(e => e.TotalCutOffMarks)
                    .HasMaxLength(50)
                    .HasColumnName("totalcutoffmarks");
                entity.Property(e => e.TotalMarksExamined).HasColumnName("totalmarksexamined");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<ExamsConfigDBO>(entity =>
            {
                entity.HasKey(e => new { e.ConfigType, e.ExamName })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("exams_config");

                entity.Property(e => e.ConfigType).HasColumnName("config_type");
                entity.Property(e => e.ExamName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''")
                    .HasColumnName("exam_name");
                entity.Property(e => e.ExamCutoffMarks)
                    .HasDefaultValueSql("'0'")
                    .HasColumnName("exam_cutoff_marks");
                entity.Property(e => e.ExamDuration).HasColumnName("exam_duration");
                entity.Property(e => e.ExamOrder).HasColumnName("exam_order");
                entity.Property(e => e.IsOmr).HasColumnName("is_omr");
                entity.Property(e => e.PrecedingExamName)
                    .HasMaxLength(400)
                    .HasColumnName("preceding_exam_name");
                entity.Property(e => e.SectionDetails)
                    .HasMaxLength(1000)
                    .HasColumnName("section_details");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1-Active 2-Inactive 3-Deleted")
                    .HasColumnName("status");
                entity.Property(e => e.SubsectionDetails)
                    .HasColumnType("text")
                    .HasColumnName("subsection_details");
            });

            modelBuilder.Entity<GetCandidateExamsDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("getcandidateexam");

                entity.Property(e => e.Address1City)
                    .HasMaxLength(50)
                    .HasColumnName("address1_city")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address1Country)
                    .HasMaxLength(50)
                    .HasColumnName("address1_country")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address1Pincode)
                    .HasMaxLength(20)
                    .HasColumnName("address1_pincode")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address1State)
                    .HasMaxLength(50)
                    .HasColumnName("address1_state")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address1Street1)
                    .HasMaxLength(50)
                    .HasColumnName("address1_street1")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address1Street2)
                    .HasMaxLength(50)
                    .HasColumnName("address1_street2")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address2City)
                    .HasMaxLength(50)
                    .HasColumnName("address2_city")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address2Country)
                    .HasMaxLength(50)
                    .HasColumnName("address2_country")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address2Pincode)
                    .HasMaxLength(20)
                    .HasColumnName("address2_pincode")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address2State)
                    .HasMaxLength(50)
                    .HasColumnName("address2_state")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address2Street1)
                    .HasMaxLength(50)
                    .HasColumnName("address2_street1")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Address2Street2)
                    .HasMaxLength(100)
                    .HasColumnName("address2_street2")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.BatchId)
                    .HasMaxLength(36)
                    .HasColumnName("batchid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.CandidateExamId)
                    .HasMaxLength(36)
                    .HasColumnName("candidateexamid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("dateofbirth");
                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .HasColumnName("emailaddress")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.EmergencyContactNumber)
                    .HasMaxLength(20)
                    .HasColumnName("emergencycontactnumber")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ExamDate).HasColumnName("examdate");
                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasColumnName("examid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ExamName)
                    .HasMaxLength(100)
                    .HasColumnName("examname")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ExamsCreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("exams_createdon");
                entity.Property(e => e.ExamsExamId)
                    .HasMaxLength(36)
                    .HasColumnName("exams_examid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ExamsExamUniqCode)
                    .HasMaxLength(20)
                    .HasColumnName("exams_examuniqcode")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ExamsModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("exams_modifiedby")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ExamsModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("exams_modifiedon");
                entity.Property(e => e.ExamsReatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("exams_reatedby")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ExamsStatus)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("exams_status");
                entity.Property(e => e.ExamTime)
                    .HasColumnType("time")
                    .HasColumnName("examtime");
                entity.Property(e => e.ExamType)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1-Free Exam 2-Paid Exam 3- Course/Batch Exam")
                    .HasColumnName("examtype");
                entity.Property(e => e.ExamUniqCode)
                    .HasMaxLength(20)
                    .HasColumnName("examuniqcode")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.FacebookId)
                    .HasMaxLength(50)
                    .HasColumnName("facebookid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstname")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Gender).HasColumnName("gender");
                entity.Property(e => e.IsCourseExam)
                    .HasDefaultValueSql("'1'")
                    .HasColumnName("iscourseexam");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastname")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.LinkedinId)
                    .HasMaxLength(50)
                    .HasColumnName("linkedinid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .HasColumnName("mobilenumber")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(20)
                    .HasColumnName("registrationumber")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Salutation)
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("salutation")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - Exam running, 2 - Exam Completed, 3 - Deative")
                    .HasColumnName("status");
                entity.Property(e => e.ThumbImpression)
                    .HasMaxLength(100)
                    .HasComment("Path of thumb impression")
                    .HasColumnName("thumbimpression")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.TraineePhoto)
                    .HasMaxLength(100)
                    .HasComment("Path of trainee photo")
                    .HasColumnName("traineephoto")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.UserDisplayId)
                    .HasMaxLength(20)
                    .HasColumnName("userdisplayid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("userid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("username")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.UsersCreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("users_createdby")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.UsersCreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("users_createdon");
                entity.Property(e => e.UsersModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("users_modifiedby")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.UsersModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("users_modifiedon");
                entity.Property(e => e.UsersStatus)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted, 0 - dummy user, Shouldnot use 9")
                    .HasColumnName("users_status");
                entity.Property(e => e.UsersUserId)
                    .HasMaxLength(36)
                    .HasColumnName("users_userid")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
                entity.Property(e => e.UserType)
                    .HasComment("0-Admin, 1-employee, 2-trainee, 3-super user, 4-trainingprovider")
                    .HasColumnName("usertype");
            });

            modelBuilder.Entity<GroupsDBO>(entity =>
            {
                entity.HasKey(e => e.GroupDisplayId).HasName("PRIMARY");

                entity
                    .ToTable("groups")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.GroupDisplayId).HasColumnName("groupdisplayid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.GroupDescription)
                    .HasComment("Passage for comprehension questions")
                    .HasColumnType("text")
                    .HasColumnName("groupdescription");
                entity.Property(e => e.GroupId)
                    .HasMaxLength(36)
                    .HasColumnName("groupid");
                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .HasColumnName("imagepath");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasColumnName("sectionid");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.SubsectionId)
                    .HasMaxLength(36)
                    .HasColumnName("subsectionid");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<LogsDBO>(entity =>
            {
                entity.HasKey(e => e.LogId).HasName("PRIMARY");

                entity.ToTable("logs");

                entity.Property(e => e.LogId)
                    .HasMaxLength(36)
                    .HasColumnName("logid");
                entity.Property(e => e.Action)
                    .HasColumnType("text")
                    .HasColumnName("action");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .HasColumnName("emailaddress");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.PageName)
                    .HasMaxLength(500)
                    .HasColumnName("pagename");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("username");
                entity.Property(e => e.UserType).HasColumnName("usertype");
            });

            modelBuilder.Entity<MarksDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("marks")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.UserId, "FK_marks");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(36)
                    .HasColumnName("courseid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.CutOffMarks)
                    .HasMaxLength(50)
                    .HasColumnName("cutoffmarks");
                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasColumnName("examid");
                entity.Property(e => e.ExamWrittenDate)
                    .HasMaxLength(20)
                    .HasColumnName("examwrittendate");
                entity.Property(e => e.Grade)
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasColumnName("grade");
                entity.Property(e => e.IsReportSent).HasColumnName("isreportsent");
                entity.Property(e => e.MarksId)
                    .HasMaxLength(36)
                    .HasColumnName("marksid");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.NegativeMarksSecured).HasColumnName("negativemarkssecured");
                entity.Property(e => e.PositiveMarksSecured).HasColumnName("positivemarkssecured");
                entity.Property(e => e.QuestionPaperId)
                    .HasMaxLength(36)
                    .HasColumnName("questionpaperid");
                entity.Property(e => e.ReportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reportdate");
                entity.Property(e => e.ResFloat1).HasColumnName("res_float1");
                entity.Property(e => e.ResFloat2).HasColumnName("res_float2");
                entity.Property(e => e.ResInt1).HasColumnName("res_int1");
                entity.Property(e => e.ResInt2).HasColumnName("res_int2");
                entity.Property(e => e.ResString1)
                    .HasMaxLength(100)
                    .HasColumnName("res_string1");
                entity.Property(e => e.ResString2)
                    .HasMaxLength(100)
                    .HasColumnName("res_string2");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.TimeRelapsed)
                    .HasMaxLength(4)
                    .HasColumnName("timerelapsed");
                entity.Property(e => e.TotalMarksExamined).HasColumnName("totalmarksexamined");
                entity.Property(e => e.TotalMarksSecured).HasColumnName("totalmarkssecured");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<MarkdetailsDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("markdetails")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ActualMarks).HasColumnName("actualmarks");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.Markdetailsid)
                    .HasMaxLength(36)
                    .HasColumnName("markdetailsid");
                entity.Property(e => e.MarksId)
                    .HasMaxLength(36)
                    .HasComment("References marks.marksid")
                    .HasColumnName("marksid");
                entity.Property(e => e.MarksSecured).HasColumnName("markssecured");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.NegativeMarks).HasColumnName("negativemarks");
                entity.Property(e => e.NumberOfQuestions)
                    .HasMaxLength(50)
                    .HasColumnName("numberofquestions");
                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasColumnName("sectionid");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.SubsectionId)
                    .HasMaxLength(36)
                    .HasColumnName("subsectionid");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<OnlineExamQuestionPapersDBO>(entity =>
            {
                entity.HasKey(e => new { e.SequenceNumber, e.QuestionPaperId, e.ExamId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity
                    .ToTable("onlineexamquestionpapers")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.TrainingProviderId, e.ExamId, e.QuestionPaperId }, "onlineexamquestionpapers_tpid_examid_qpid");

                entity.Property(e => e.SequenceNumber).HasColumnName("sequencenumber");
                entity.Property(e => e.QuestionPaperId).HasColumnName("questionpaperid");
                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("examid");
                entity.Property(e => e.Answer)
                    .HasMaxLength(1)
                    .HasComment("It contains one of four opions i.e a,b,c,d")
                    .HasColumnName("answer");
                entity.Property(e => e.AnswerFilePath)
                    .HasMaxLength(100)
                    .HasColumnName("answerfilepath");
                entity.Property(e => e.AnswerFormat)
                    .HasMaxLength(30)
                    .HasColumnName("answerformat");
                entity.Property(e => e.Complexity).HasColumnName("complexity");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.DescriptiveAnswer)
                    .HasColumnType("text")
                    .HasColumnName("descriptiveanswer");
                entity.Property(e => e.DescriptiveAnswerExplanation)
                    .HasColumnType("text")
                    .HasColumnName("descriptiveanswerexplanation");
                entity.Property(e => e.ExamOrCourseId)
                    .HasMaxLength(36)
                    .HasColumnName("examorcourseid");
                entity.Property(e => e.GroupDescription)
                    .HasComment("For comprehension questions, Actual passage is in groups(groupdescription) table and references groups(groupid) ")
                    .HasColumnType("text")
                    .HasColumnName("groupdescription");
                entity.Property(e => e.GroupDisplayId).HasColumnName("groupdisplayid");
                entity.Property(e => e.GroupId)
                    .HasMaxLength(36)
                    .HasColumnName("groupid");
                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .HasColumnName("imagepath");
                entity.Property(e => e.IsOmr)
                    .HasComment("1 - the questionis of OMR type, 2- the questionis non OMR type")
                    .HasColumnName("IsOMR");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.NegativeMarksPerQuestion).HasColumnName("negativemarksperquestion");
                entity.Property(e => e.Option1)
                    .HasMaxLength(250)
                    .HasColumnName("option1");
                entity.Property(e => e.Option1FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option1filepath");
                entity.Property(e => e.Option2)
                    .HasMaxLength(250)
                    .HasColumnName("option2");
                entity.Property(e => e.Option2FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option2filepath");
                entity.Property(e => e.Option3)
                    .HasMaxLength(250)
                    .HasColumnName("option3");
                entity.Property(e => e.Option3FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option3filepath");
                entity.Property(e => e.Option4)
                    .HasMaxLength(250)
                    .HasColumnName("option4");
                entity.Property(e => e.Option4FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option4filepath");
                entity.Property(e => e.PositiveMarksPerQuestion).HasColumnName("positivemarksperquestion");
                entity.Property(e => e.QuestionDescription)
                    .HasColumnType("text")
                    .HasColumnName("questiondescription");
                entity.Property(e => e.QuestionFilePath)
                    .HasMaxLength(100)
                    .HasColumnName("questionfilepath");
                entity.Property(e => e.QuestionHint)
                    .HasColumnType("text")
                    .HasColumnName("questionhint");
                entity.Property(e => e.QuestionId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("questionid");
                entity.Property(e => e.SectionDescription)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''")
                    .HasColumnName("sectiondescription");
                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("sectionid");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.SubsectionDescription)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''")
                    .HasColumnName("subsectiondescription");
                entity.Property(e => e.SubsectionId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("subsectionid");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<OnlineExamSubmittedAnswersDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("onlineexamsubmittedanswers")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.CorrectAnswer)
                    .HasMaxLength(100)
                    .HasColumnName("correctanswer");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasColumnName("examid");
                entity.Property(e => e.NegativeMarks)
                    .HasColumnType("float(5,2)")
                    .HasColumnName("negativemarks");
                entity.Property(e => e.ObtainedMarks)
                    .HasColumnType("float(5,2)")
                    .HasColumnName("obtainedmarks");
                entity.Property(e => e.PositiveMarks)
                    .HasColumnType("float(5,2)")
                    .HasColumnName("positivemarks");
                entity.Property(e => e.QuestionId)
                    .HasMaxLength(36)
                    .HasColumnName("questionid");
                entity.Property(e => e.QuestionNumber).HasColumnName("questionnumber");
                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasColumnName("sectionid");
                entity.Property(e => e.SubmittedAnswer)
                    .HasMaxLength(50)
                    .HasColumnName("submittedanswer");
                entity.Property(e => e.SubmittedTime)
                    .HasColumnType("time")
                    .HasColumnName("submittedtime");
                entity.Property(e => e.SubsectionId)
                    .HasMaxLength(36)
                    .HasColumnName("subsectionid");
                entity.Property(e => e.TableName)
                    .HasMaxLength(100)
                    .HasColumnName("tablename");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<PrintDetailsDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("printdetails")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(36)
                    .HasColumnName("courseid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasColumnName("examid");
                entity.Property(e => e.Instructions)
                    .HasColumnType("text")
                    .HasColumnName("instructions");
                entity.Property(e => e.IsOnline)
                    .HasDefaultValueSql("'0'")
                    .HasColumnName("isonline");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.PrintDetailId)
                    .HasMaxLength(36)
                    .HasColumnName("printdetailid");
                entity.Property(e => e.QuestionPaperId)
                    .HasMaxLength(36)
                    .HasColumnName("questionpaperid");
                entity.Property(e => e.QuestionPaperPath)
                    .HasMaxLength(100)
                    .HasColumnName("questionpaperpath");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<QualificationsDBO>(entity =>
            {
                entity.HasKey(e => e.QualificationId).HasName("PRIMARY");

                entity
                    .ToTable("qualifications")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.UserId, "FK_qualifications");

                entity.Property(e => e.QualificationId)
                    .HasMaxLength(36)
                    .HasColumnName("qualificationid");
                entity.Property(e => e.BoardUniversity)
                    .HasMaxLength(50)
                    .HasColumnName("board_university");
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");
                entity.Property(e => e.CompleteStatus)
                    .HasComment("1-passed 2- failed 3- discontinued")
                    .HasColumnName("completestatus");
                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.MarksSecured).HasColumnName("markssecured");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.PassedOn).HasColumnName("passedon");
                entity.Property(e => e.Percentage).HasColumnName("percentage");
                entity.Property(e => e.Pincode)
                    .HasMaxLength(20)
                    .HasColumnName("pincode");
                entity.Property(e => e.QualificationLevel)
                    .HasComment("1 - School 2 - Intermediate 3 - Graduation 4 - Post Graduation")
                    .HasColumnName("qualification_level");
                entity.Property(e => e.QualificationName)
                    .HasMaxLength(50)
                    .HasColumnName("qualificationname");
                entity.Property(e => e.ResFloat1).HasColumnName("res_float1");
                entity.Property(e => e.ResFloat2).HasColumnName("res_float2");
                entity.Property(e => e.ResInt1).HasColumnName("res_int1");
                entity.Property(e => e.ResInt2).HasColumnName("res_int2");
                entity.Property(e => e.ResString1)
                    .HasMaxLength(100)
                    .HasColumnName("res_string1");
                entity.Property(e => e.ResString2)
                    .HasMaxLength(100)
                    .HasColumnName("res_string2");
                entity.Property(e => e.SchoolCollageName)
                    .HasMaxLength(200)
                    .HasColumnName("school_collagename");
                entity.Property(e => e.Specialization)
                    .HasMaxLength(50)
                    .HasColumnName("specialization");
                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .HasColumnName("state");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.Street1)
                    .HasMaxLength(50)
                    .HasColumnName("street1");
                entity.Property(e => e.Street2)
                    .HasMaxLength(50)
                    .HasColumnName("street2");
                entity.Property(e => e.TotalMarks).HasColumnName("totalmarks");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<QuestionsDBO>(entity =>
            {
                entity.HasKey(e => new { e.TrainingProviderId, e.SectionId, e.SubsectionId, e.QuestionId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity
                    .ToTable("questions")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("sectionid");
                entity.Property(e => e.SubsectionId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("subsectionid");
                entity.Property(e => e.QuestionId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("questionid");
                entity.Property(e => e.Answer)
                    .HasMaxLength(1)
                    .HasComment("It contains one of four opions i.e a,b,c,d")
                    .HasColumnName("answer");
                entity.Property(e => e.AnswerFilePath)
                    .HasMaxLength(100)
                    .HasColumnName("answerfilepath");
                entity.Property(e => e.AnswerFormat)
                    .HasMaxLength(30)
                    .HasColumnName("answerformat");
                entity.Property(e => e.Complexity).HasColumnName("complexity");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.DescriptiveAnswer)
                    .HasColumnType("text")
                    .HasColumnName("descriptiveanswer");
                entity.Property(e => e.DescriptiveAnswerExplanation)
                    .HasColumnType("text")
                    .HasColumnName("descriptiveanswerexplanation");
                entity.Property(e => e.ExamorCourseId).HasMaxLength(36);
                entity.Property(e => e.GroupId)
                    .HasMaxLength(36)
                    .HasComment("For comprehension questions, Actual passage is in groups(groupdescription) table and references groups(groupid) ")
                    .HasColumnName("groupid");
                entity.Property(e => e.IsOmr)
                    .HasComment("1 - the questionis of OMR type, 2- the questionis non OMR type")
                    .HasColumnName("IsOMR");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Option1)
                    .HasMaxLength(250)
                    .HasColumnName("option1");
                entity.Property(e => e.Option1FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option1filepath");
                entity.Property(e => e.Option2)
                    .HasMaxLength(250)
                    .HasColumnName("option2");
                entity.Property(e => e.Option2FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option2filepath");
                entity.Property(e => e.Option3)
                    .HasMaxLength(250)
                    .HasColumnName("option3");
                entity.Property(e => e.Option3FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option3filepath");
                entity.Property(e => e.Option4)
                    .HasMaxLength(250)
                    .HasColumnName("option4");
                entity.Property(e => e.Option4FilePath)
                    .HasMaxLength(100)
                    .HasColumnName("option4filepath");
                entity.Property(e => e.QuestionDescription)
                    .HasColumnType("text")
                    .HasColumnName("questiondescription");
                entity.Property(e => e.QuestionFilePath)
                    .HasMaxLength(100)
                    .HasColumnName("questionfilepath");
                entity.Property(e => e.QuestionHint)
                    .HasColumnType("text")
                    .HasColumnName("questionhint");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
            });

            modelBuilder.Entity<QuestiondetailsDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("questiondetails")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.TrainingProviderId, e.ExamId, e.QuestionPaperId }, "questiondetails_tpid_examid_qpid");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(36)
                    .HasColumnName("courseid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.CutOffMarks)
                    .HasMaxLength(50)
                    .HasColumnName("cutoffmarks");
                entity.Property(e => e.ExamId)
                    .HasMaxLength(36)
                    .HasColumnName("examid");
                entity.Property(e => e.GroupCount).HasColumnName("groupcount");
                entity.Property(e => e.IsOmr).HasColumnName("isomr");
                entity.Property(e => e.IsOnline)
                    .HasDefaultValueSql("'0'")
                    .HasColumnName("isonline");
                entity.Property(e => e.MarksExamined)
                    .HasMaxLength(50)
                    .HasColumnName("marksexamined");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.NegativeMarks).HasColumnName("negativemarks");
                entity.Property(e => e.NumberOfQuestions)
                    .HasMaxLength(50)
                    .HasColumnName("numberofquestions");
                entity.Property(e => e.QuestionDetailId)
                    .HasMaxLength(36)
                    .HasColumnName("questiondetailid");
                entity.Property(e => e.QuestionPaperId)
                    .HasMaxLength(36)
                    .HasColumnName("questionpaperid");
                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasColumnName("sectionid");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.SubsectionId)
                    .HasMaxLength(36)
                    .HasColumnName("subsectionid");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<SectionsDBO>(entity =>
            {
                entity.HasKey(e => e.SectionId).HasName("PRIMARY");

                entity
                    .ToTable("sections")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasColumnName("sectionid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.SectionDescription)
                    .HasMaxLength(100)
                    .HasColumnName("sectiondescription");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.Suggestions)
                    .HasMaxLength(400)
                    .HasColumnName("suggestions");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<ServicesDBO>(entity =>
            {
                entity.HasKey(e => e.ServiceId).HasName("PRIMARY");

                entity
                    .ToTable("services")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ServiceId)
                    .HasMaxLength(36)
                    .HasColumnName("serviceid");
                entity.Property(e => e.Amount)
                    .HasComment("1-FreeExam 0-PaidExam")
                    .HasColumnName("amount");
                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");
                entity.Property(e => e.ChildServiceId)
                    .HasComment("stores multiple serviceids seperated with comma")
                    .HasColumnType("text")
                    .HasColumnName("childserviceid");
                entity.Property(e => e.ClassHours).HasColumnName("classhours");
                entity.Property(e => e.CourseDuration).HasColumnName("courseduration");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
                entity.Property(e => e.Faculty)
                    .HasMaxLength(36)
                    .HasComment("employeeid")
                    .HasColumnName("faculty");
                entity.Property(e => e.Fees).HasColumnName("fees");
                entity.Property(e => e.IsExam).HasComment("2- exam 1- course");
                entity.Property(e => e.LabHours).HasColumnName("labhours");
                entity.Property(e => e.MaxNumber).HasColumnName("maxnumber");
                entity.Property(e => e.MinNumber).HasColumnName("minnumber");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.ResFloat1).HasColumnName("res_float1");
                entity.Property(e => e.ResFloat2).HasColumnName("res_float2");
                entity.Property(e => e.ResInt1).HasColumnName("res_int1");
                entity.Property(e => e.ResInt2).HasColumnName("res_int2");
                entity.Property(e => e.ResString1)
                    .HasMaxLength(100)
                    .HasColumnName("res_string1");
                entity.Property(e => e.ResString2)
                    .HasMaxLength(100)
                    .HasColumnName("res_string2");
                entity.Property(e => e.ServiceName)
                    .HasMaxLength(200)
                    .HasColumnName("servicename");
                entity.Property(e => e.ServiceType)
                    .HasComment("1- single course 2- multiple coureses")
                    .HasColumnName("servicetype");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<StudentdetailsDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("studentdetails")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Branch)
                    .HasMaxLength(50)
                    .HasColumnName("branch");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .HasColumnName("emailid");
                entity.Property(e => e.FirstYearBackLogs).HasColumnName("firstyearbacklogs");
                entity.Property(e => e.FirstYearPercentage).HasColumnName("firstyearpercentage");
                entity.Property(e => e.HallTicketNumber)
                    .HasMaxLength(20)
                    .HasComment("hall ticket number is unique for every candidate")
                    .HasColumnName("hallticketnumber");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.OverallPercentage).HasColumnName("overallpercentage");
                entity.Property(e => e.SecondYearBackLogs).HasColumnName("secondyearbacklogs");
                entity.Property(e => e.SecondYearPercentage).HasColumnName("secondyearpercentage");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.ThirdYearBackLogs).HasColumnName("thirdyearbacklogs");
                entity.Property(e => e.ThirdYearPercentage).HasColumnName("thirdyearpercentage");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("''")
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<SubscribedTrainingsDBO>(entity =>
            {
                entity.HasKey(e => e.TrainingId).HasName("PRIMARY");

                entity
                    .ToTable("subscribed_training")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.TrainingId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingid");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.BatchId)
                    .HasColumnType("text")
                    .HasColumnName("batchid");
                entity.Property(e => e.CourseId)
                    .HasMaxLength(36)
                    .HasComment("services id for course")
                    .HasColumnName("courseid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.ResFloat1).HasColumnName("res_float1");
                entity.Property(e => e.ResFloat2).HasColumnName("res_float2");
                entity.Property(e => e.ResInt1).HasColumnName("res_int1");
                entity.Property(e => e.ResInt2).HasColumnName("res_int2");
                entity.Property(e => e.ResString1)
                    .HasMaxLength(100)
                    .HasColumnName("res_string1");
                entity.Property(e => e.ResString2)
                    .HasMaxLength(100)
                    .HasColumnName("res_string2");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<SubsectionsDBO>(entity =>
            {
                entity.HasKey(e => e.SubsectionId).HasName("PRIMARY");

                entity
                    .ToTable("subsections")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.SubsectionId)
                    .HasMaxLength(36)
                    .HasColumnName("subsectionid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.SectionId)
                    .HasMaxLength(36)
                    .HasColumnName("sectionid");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.SubsectionDescription)
                    .HasMaxLength(100)
                    .HasColumnName("subsectiondescription");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
            });

            modelBuilder.Entity<TestUserRegistrationdetailsDBO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("test__user_registrationdetails")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.FailedDbReason)
                    .HasMaxLength(2000)
                    .HasColumnName("failed_db_reason");
                entity.Property(e => e.HtNo)
                    .HasMaxLength(50)
                    .HasColumnName("htno");
                entity.Property(e => e.InsertStmt)
                    .HasColumnType("text")
                    .HasColumnName("insert_stmt");
            });

            modelBuilder.Entity<ThemeTemplatesDBO>(entity =>
            {
                entity.HasKey(e => e.ThemeTemplateId).HasName("PRIMARY");

                entity
                    .ToTable("themetemplates")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ThemeTemplateId).HasColumnName("themetemplateid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
                entity.Property(e => e.ThemeTemplateName)
                    .HasMaxLength(100)
                    .HasColumnName("themetemplatename");
                entity.Property(e => e.ThemeTemplatePath)
                    .HasMaxLength(100)
                    .HasColumnName("themetemplatepath");
            });

            modelBuilder.Entity<TrainingprovidersDBO>(entity =>
            {
                entity.HasKey(e => e.TrainingProviderId).HasName("PRIMARY");

                entity
                    .ToTable("trainingprovider")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted")
                    .HasColumnName("status");
            });

            modelBuilder.Entity<UsersDBO>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PRIMARY");

                entity
                    .ToTable("users")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("userid");
                entity.Property(e => e.Address1City)
                    .HasMaxLength(50)
                    .HasColumnName("address1_city");
                entity.Property(e => e.Address1Country)
                    .HasMaxLength(50)
                    .HasColumnName("address1_country");
                entity.Property(e => e.Address1Pincode)
                    .HasMaxLength(20)
                    .HasColumnName("address1_pincode");
                entity.Property(e => e.Address1State)
                    .HasMaxLength(50)
                    .HasColumnName("address1_state");
                entity.Property(e => e.Address1Street1)
                    .HasMaxLength(50)
                    .HasColumnName("address1_street1");
                entity.Property(e => e.Address1Street2)
                    .HasMaxLength(50)
                    .HasColumnName("address1_street2");
                entity.Property(e => e.Address2City)
                    .HasMaxLength(50)
                    .HasColumnName("address2_city");
                entity.Property(e => e.Address2Country)
                    .HasMaxLength(50)
                    .HasColumnName("address2_country");
                entity.Property(e => e.Address2Pincode)
                    .HasMaxLength(20)
                    .HasColumnName("address2_pincode");
                entity.Property(e => e.Address2State)
                    .HasMaxLength(50)
                    .HasColumnName("address2_state");
                entity.Property(e => e.Address2Street1)
                    .HasMaxLength(50)
                    .HasColumnName("address2_street1");
                entity.Property(e => e.Address2Street2)
                    .HasMaxLength(100)
                    .HasColumnName("address2_street2");
                entity.Property(e => e.CandidateComments)
                    .HasMaxLength(500)
                    .HasColumnName("candidatecomments");
                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .HasColumnName("comments");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .HasColumnName("createdby");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdon");
                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("dateofbirth");
                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .HasColumnName("emailaddress");
                entity.Property(e => e.EmergencyContactNumber)
                    .HasMaxLength(20)
                    .HasColumnName("emergencycontactnumber");
                entity.Property(e => e.FacebookId)
                    .HasMaxLength(50)
                    .HasColumnName("facebookid");
                entity.Property(e => e.Features)
                    .HasColumnType("text")
                    .HasColumnName("features");
                entity.Property(e => e.FingerPrintValue)
                    .HasMaxLength(100)
                    .HasComment("Path of finger print")
                    .HasColumnName("fingerprintvalue");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");
                entity.Property(e => e.Gender).HasColumnName("gender");
                entity.Property(e => e.HallTicket)
                    .HasMaxLength(20)
                    .HasColumnName("hallticket");
                entity.Property(e => e.IsAcceptedTermsAndConditions).HasColumnName("isacceptedtermsandconditions");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");
                entity.Property(e => e.LinkedinId)
                    .HasMaxLength(50)
                    .HasColumnName("linkedinid");
                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .HasColumnName("mobilenumber");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(36)
                    .HasColumnName("modifiedby");
                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedon");
                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");
                entity.Property(e => e.ProfilePicPath)
                    .HasMaxLength(100)
                    .HasComment("Path of profile picture")
                    .HasColumnName("profilepicpath");
                entity.Property(e => e.QuestionPaperSetNumber).HasColumnName("questionpapersetnumber");
                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(20)
                    .HasColumnName("registrationumber");
                entity.Property(e => e.ResFloat1).HasColumnName("res_float1");
                entity.Property(e => e.ResFloat2).HasColumnName("res_float2");
                entity.Property(e => e.ResInt1).HasColumnName("res_int1");
                entity.Property(e => e.ResInt2).HasColumnName("res_int2");
                entity.Property(e => e.ResString1)
                    .HasMaxLength(100)
                    .HasColumnName("res_string1");
                entity.Property(e => e.ResString2)
                    .HasMaxLength(100)
                    .HasColumnName("res_string2");
                entity.Property(e => e.Salutation)
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("salutation");
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 - active, 2 - deactive, 3 - deleted, 0 - dummy user, Shouldnot use 9")
                    .HasColumnName("status");
                entity.Property(e => e.ThumbImpression)
                    .HasMaxLength(100)
                    .HasComment("Path of thumb impression")
                    .HasColumnName("thumbimpression");
                entity.Property(e => e.TraineePhoto)
                    .HasMaxLength(100)
                    .HasComment("Path of trainee photo")
                    .HasColumnName("traineephoto");
                entity.Property(e => e.TrainingProviderId)
                    .HasMaxLength(36)
                    .HasColumnName("trainingproviderid");
                entity.Property(e => e.UnSubscribe).HasColumnName("unsubscribe");
                entity.Property(e => e.UserDisplayId)
                    .HasMaxLength(20)
                    .HasColumnName("userdisplayid");
                entity.Property(e => e.UserType)
                    .HasComment("0-Admin, 1-employee, 2-trainee, 3-super user, 4-trainingprovider")
                    .HasColumnName("usertype");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
