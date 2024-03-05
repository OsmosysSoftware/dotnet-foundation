using System;
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
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05d341d9-ee1a-4357-ad46-60633e1f60a6", null, "DEVELOPER", "DEVELOPER" },
                    { "343abb08-4cbd-4a9b-93e1-dca377b5d984", null, "LEAD", "LEAD" },
                    { "c08d9a4b-72f5-4eab-8c9a-6fb17a3e92a1", null, "SUPERADMIN", "SUPERADMIN" },
                    { "e2136d42-58a2-4b50-ad76-a34466d2d3d5", null, "ADMIN", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b109c28a-6c6f-43d2-bc49-9fba25cb6e72", 0, "385aeb70-97c7-4bc6-becc-f2668afce720", "admin@osmox.co", true, false, null, "ADMIN@OSMOX.CO", "ADMIN@OSMOX.CO", "AQAAAAIAAYagAAAAENAt0HNxZQuZEe7wQ42pp7gaDsOIxrrFmmgegH6h0E4HrGCtDDS0O7iZ3CHzjKznOw==", null, false, "f7a1d5fd-4265-4be5-adeb-e20eb1f8d9d3", false, "admin@osmox.co" });

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
                values: new object[] { 1, "India", 1, new DateTime(2024, 3, 5, 16, 53, 24, 172, DateTimeKind.Utc).AddTicks(4141), "admin@osmox.co", "Super", "b109c28a-6c6f-43d2-bc49-9fba25cb6e72", "Admin", 1, new DateTime(2024, 3, 5, 16, 53, 24, 172, DateTimeKind.Utc).AddTicks(4142), "0000000000", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05d341d9-ee1a-4357-ad46-60633e1f60a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "343abb08-4cbd-4a9b-93e1-dca377b5d984");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2136d42-58a2-4b50-ad76-a34466d2d3d5");

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
