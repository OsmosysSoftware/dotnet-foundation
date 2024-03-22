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
        string claimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

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
            NormalizedEmail = superAdminEmail.ToUpper(),
            ConcurrencyStamp = "26085b3f-071d-4feb-bf47-a58d1b1b2ad4",
            SecurityStamp = "c496f24f-7e1b-449c-90d3-885490a07388"
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
            CreatedOn = new DateTime(2024, 3, 8, 10, 54, 33, 198, DateTimeKind.Utc),
            ModifiedBy = 1,
            ModifiedOn = new DateTime(2024, 3, 8, 10, 54, 33, 198, DateTimeKind.Utc),
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
