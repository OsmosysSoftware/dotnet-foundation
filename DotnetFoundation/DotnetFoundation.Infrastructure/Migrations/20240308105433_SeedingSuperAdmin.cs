using System;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetFoundation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region variables to be imported from .env files
            // Get superadmin variables
            string superAdminRoleId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ROLE_ID") ?? throw new Exception("No SUPER_ADMIN_ROLE_ID specified");
            string superAdminId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ID") ?? throw new Exception("No SUPER_ADMIN_ID specified");
            string superAdminEmail = Environment.GetEnvironmentVariable("SUPER_ADMIN_EMAIL") ?? throw new Exception("No SUPER_ADMIN_EMAIL specified");
            string superAdminPassword = Environment.GetEnvironmentVariable("SUPER_ADMIN_PASSWORD") ?? throw new Exception("No SUPER_ADMIN_PASSWORD specified");
            string claimType = Environment.GetEnvironmentVariable("CLAIM_TYPE") ?? throw new Exception("No CLAIM_TYPE specified");

            PasswordHasher<IdentityApplicationUser> hasher = new();
            string hashedPassword = hasher.HashPassword(null, superAdminPassword);
            #endregion

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { superAdminRoleId, null, "SUPERADMIN", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { superAdminId, 0, "26085b3f-071d-4feb-bf47-a58d1b1b2ad4", superAdminEmail, true, false, null, superAdminEmail.ToUpper(), superAdminEmail.ToUpper(), hashedPassword, null, false, "c496f24f-7e1b-449c-90d3-885490a07388", false, superAdminEmail });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, claimType, "SUPERADMIN", superAdminId });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { superAdminRoleId, superAdminId });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Country", "CreatedBy", "CreatedOn", "Email", "FirstName", "IdentityApplicationUserId", "LastName", "ModifiedBy", "ModifiedOn", "PhoneNumber", "Status" },
                values: new object[] { 1, "India", 1, new DateTime(2024, 3, 8, 10, 54, 33, 198, DateTimeKind.Utc).AddTicks(5932), superAdminEmail, "Super", superAdminId, "Admin", 1, new DateTime(2024, 3, 8, 10, 54, 33, 198, DateTimeKind.Utc).AddTicks(5933), "0000000000", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            #region variables to be imported from .env files
            // Get superadmin variables
            string superAdminRoleId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ROLE_ID") ?? throw new Exception("No SUPER_ADMIN_ROLE_ID specified");
            string superAdminId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ID") ?? throw new Exception("No SUPER_ADMIN_ID specified");
            #endregion

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { superAdminRoleId, superAdminId });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: superAdminRoleId);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: superAdminId);
        }
    }
}
