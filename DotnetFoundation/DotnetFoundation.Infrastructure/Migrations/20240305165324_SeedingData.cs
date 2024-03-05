using System;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetFoundation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region variables to be imported from .env files
            // Get Role Ids
            string developerRoleId = Environment.GetEnvironmentVariable("DEVELOPER_ROLE_ID") ?? throw new Exception("No DEVELOPER_ROLE_ID specified");
            string leadRoleId = Environment.GetEnvironmentVariable("LEAD_ROLE_ID") ?? throw new Exception("No LEAD_ROLE_ID specified");
            string adminRoleId = Environment.GetEnvironmentVariable("ADMIN_ROLE_ID") ?? throw new Exception("No ADMIN_ROLE_ID specified");
            string superAdminRoleId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ROLE_ID") ?? throw new Exception("No SUPER_ADMIN_ROLE_ID specified");

            // Get superadmin variables
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
                values: new object[,]
                {
                    { developerRoleId, null, "DEVELOPER", "DEVELOPER" },
                    { leadRoleId, null, "LEAD", "LEAD" },
                    { adminRoleId, null, "ADMIN", "ADMIN" },
                    { superAdminRoleId, null, "SUPERADMIN", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { superAdminId, 0, "385aeb70-97c7-4bc6-becc-f2668afce720", superAdminEmail, true, false, null, superAdminEmail.ToUpper(), superAdminEmail.ToUpper(), hashedPassword, null, false, "f7a1d5fd-4265-4be5-adeb-e20eb1f8d9d3", false, superAdminEmail });

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
                values: new object[] { 1, "India", 1, new DateTime(2024, 3, 5, 16, 53, 24, 172, DateTimeKind.Utc).AddTicks(4141), superAdminEmail, "Super", superAdminId, "Admin", 1, new DateTime(2024, 3, 5, 16, 53, 24, 172, DateTimeKind.Utc).AddTicks(4142), "0000000000", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            #region Ids to be imported from .env files
            string developerRoleId = Environment.GetEnvironmentVariable("DEVELOPER_ROLE_ID") ?? throw new Exception("No DEVELOPER_ROLE_ID specified");
            string leadRoleId = Environment.GetEnvironmentVariable("LEAD_ROLE_ID") ?? throw new Exception("No LEAD_ROLE_ID specified");
            string adminRoleId = Environment.GetEnvironmentVariable("ADMIN_ROLE_ID") ?? throw new Exception("No ADMIN_ROLE_ID specified");
            string superAdminRoleId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ROLE_ID") ?? throw new Exception("No SUPER_ADMIN_ROLE_ID specified");
            string superAdminId = Environment.GetEnvironmentVariable("SUPER_ADMIN_ID") ?? throw new Exception("No SUPER_ADMIN_ID specified");     
            #endregion

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: developerRoleId);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: leadRoleId);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: adminRoleId);

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
