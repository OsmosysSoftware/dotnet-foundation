using DotnetFoundation.Domain.Enums;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotnetFoundation.Infrastructure.DatabaseContext;
public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // Get superadmin variables
        string superAdminRoleId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ROLE_ID") ?? throw new Exception("No SUPER_ADMIN_ROLE_ID specified");
        string superAdminId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ID") ?? throw new Exception("No SUPER_ADMIN_ID specified");
        string superAdminEmail = Environment.GetEnvironmentVariable("SUPER_ADMIN_EMAIL") ?? throw new Exception("No SUPER_ADMIN_EMAIL specified");
        string superAdminPassword = Environment.GetEnvironmentVariable("SUPER_ADMIN_PASSWORD") ?? throw new Exception("No SUPER_ADMIN_PASSWORD specified");
        string claimType = Environment.GetEnvironmentVariable("CLAIM_TYPE") ?? throw new Exception("No CLAIM_TYPE specified");

        // Seeding roles
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = superAdminRoleId,
                Name = Roles.SUPERADMIN.ToString().ToUpper(),
                NormalizedName = Roles.SUPERADMIN.ToString().ToUpper()
            }
        );

        IdentityApplicationUser identityApplicationUser = new()
        {
            Id = superAdminId,
            UserName = superAdminEmail,
            Email = superAdminEmail,
            EmailConfirmed = true,
            NormalizedUserName = superAdminEmail.ToUpper(),
            NormalizedEmail = superAdminEmail.ToUpper()
        };

        // Hasher to hash the password before seeding the user to the db
        PasswordHasher<IdentityApplicationUser> hasher = new();
        identityApplicationUser.PasswordHash = hasher.HashPassword(identityApplicationUser, superAdminPassword);

        // Seeding a superadmin user to AspNetUsers table
        modelBuilder.Entity<IdentityApplicationUser>().HasData(identityApplicationUser);

        // Seeding the relation between our user and role to AspNetUserRoles table
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId
            }
        );

        // Adding superadmin in users table
        ApplicationUser applicationUser = new()
        {
            Id = 1,
            FirstName = "Super",
            LastName = "Admin",
            Country = "India",
            PhoneNumber = "0000000000",
            Email = superAdminEmail,
            IdentityApplicationUserId = identityApplicationUser.Id,
            CreatedBy = 1,
            CreatedOn = DateTime.UtcNow,
            ModifiedBy = 1,
            ModifiedOn = DateTime.UtcNow,
            Status = Status.ACTIVE
        };

        modelBuilder.Entity<ApplicationUser>().HasData(applicationUser);

        // Adding superadmin in AspNetUserClaims table
        modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
            new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = superAdminId,
                ClaimType = claimType,
                ClaimValue = Roles.SUPERADMIN.ToString().ToUpper()
            }
        );
    }
}
