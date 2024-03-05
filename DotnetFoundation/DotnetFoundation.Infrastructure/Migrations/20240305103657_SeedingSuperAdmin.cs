using System;
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
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c08d9a4b-72f5-4eab-8c9a-6fb17a3e92a1", null, "SUPERADMIN", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b109c28a-6c6f-43d2-bc49-9fba25cb6e72", 0, "a6e8eb2a-663f-462c-ad93-15e6eedd6d51", "admin@osmox.co", true, false, null, "ADMIN@OSMOX.CO", "ADMIN@OSMOX.CO", "AQAAAAIAAYagAAAAEBZe2umUuQwh6SVsiq9M6NdghOTqVt1Ce2LkvAaIABuTZevSFqoX/NuEPXCIy9g2RQ==", null, false, "ffab8fa2-2906-4a19-bcd9-ffb4dcd39933", false, "admin@osmox.co" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SUPERADMIN", "b109c28a-6c6f-43d2-bc49-9fba25cb6e72" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c08d9a4b-72f5-4eab-8c9a-6fb17a3e92a1", "b109c28a-6c6f-43d2-bc49-9fba25cb6e72" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Country", "CreatedBy", "CreatedOn", "Email", "FirstName", "IdentityApplicationUserId", "LastName", "ModifiedBy", "ModifiedOn", "PhoneNumber", "Status" },
                values: new object[] { 1, "India", 1, new DateTime(2024, 3, 5, 10, 36, 56, 578, DateTimeKind.Utc).AddTicks(9597), "admin@osmox.co", "Super", "b109c28a-6c6f-43d2-bc49-9fba25cb6e72", "Admin", 1, new DateTime(2024, 3, 5, 10, 36, 56, 578, DateTimeKind.Utc).AddTicks(9598), "0000000000", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c08d9a4b-72f5-4eab-8c9a-6fb17a3e92a1", "b109c28a-6c6f-43d2-bc49-9fba25cb6e72" });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c08d9a4b-72f5-4eab-8c9a-6fb17a3e92a1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b109c28a-6c6f-43d2-bc49-9fba25cb6e72");
        }
    }
}
