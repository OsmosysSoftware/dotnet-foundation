using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetFoundation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "676d8532-6650-4323-bc71-f4047782136f", null, "SUPERADMIN", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a41617a2-e65b-4560-b70c-9727e393cd98", 0, "26085b3f-071d-4feb-bf47-a58d1b1b2ad4", "admin@osmox.com", true, false, null, "ADMIN@OSMOX.COM", "ADMIN@OSMOX.COM", "AQAAAAIAAYagAAAAEFDzmRdDmebZIXyg8o5ETbSALfTUZ18lLpuaHKgLqTXREIDt4V5jVTIPjL2IwXlb1w==", null, false, "c496f24f-7e1b-449c-90d3-885490a07388", false, "admin@osmox.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SUPERADMIN", "a41617a2-e65b-4560-b70c-9727e393cd98" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "676d8532-6650-4323-bc71-f4047782136f", "a41617a2-e65b-4560-b70c-9727e393cd98" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Country", "CreatedBy", "CreatedOn", "Email", "FirstName", "IdentityApplicationUserId", "LastName", "ModifiedBy", "ModifiedOn", "PhoneNumber", "Status" },
                values: new object[] { 1, "India", 1, new DateTime(2024, 3, 8, 10, 54, 33, 198, DateTimeKind.Utc), "admin@osmox.com", "Super", "a41617a2-e65b-4560-b70c-9727e393cd98", "Admin", 1, new DateTime(2024, 3, 8, 10, 54, 33, 198, DateTimeKind.Utc), "0000000000", 1 });
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
                keyValues: new object[] { "676d8532-6650-4323-bc71-f4047782136f", "a41617a2-e65b-4560-b70c-9727e393cd98" });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "676d8532-6650-4323-bc71-f4047782136f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a41617a2-e65b-4560-b70c-9727e393cd98");
        }
    }
}
