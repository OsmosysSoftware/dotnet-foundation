using Microsoft.EntityFrameworkCore;
using DotnetFoundation.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DotnetFoundation.DAL.DatabaseContext
{
    public partial class SqlDatabaseContext : IdentityDbContext<UsersDBO>
    {
        public SqlDatabaseContext()
        {
        }

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options)
            : base(options)
        {
        }


        public DbSet<UsersDBO> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(e => e.UserId);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(e => e.RoleId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(e => new { e.LoginProvider, e.UserId });

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
