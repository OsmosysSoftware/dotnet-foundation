using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetFoundation.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class CreateTaskDetails : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.UpdateData(
              table: "users",
              keyColumn: "FirstName",
              keyValue: null,
              column: "FirstName",
              value: "");

          migrationBuilder.AlterColumn<string>(
              name: "FirstName",
              table: "users",
              type: "longtext",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "longtext",
              oldNullable: true)
              .Annotation("MySql:CharSet", "utf8mb4")
              .OldAnnotation("MySql:CharSet", "utf8mb4");

          migrationBuilder.CreateTable(
              name: "taskdetails",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                  Description = table.Column<string>(type: "longtext", nullable: false)
                      .Annotation("MySql:CharSet", "utf8mb4"),
                  BudgetedHours = table.Column<int>(type: "int", nullable: false),
                  AssignedTo = table.Column<int>(type: "int", nullable: false),
                  Category = table.Column<string>(type: "longtext", nullable: true)
                      .Annotation("MySql:CharSet", "utf8mb4"),
                  Status = table.Column<int>(type: "int", nullable: false),
                  CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                  CreatedBy = table.Column<int>(type: "int", nullable: false),
                  ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                  ModifiedBy = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_taskdetails", x => x.Id);
              })
              .Annotation("MySql:CharSet", "utf8mb4");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "taskdetails");

          migrationBuilder.AlterColumn<string>(
              name: "FirstName",
              table: "users",
              type: "longtext",
              nullable: true,
              oldClrType: typeof(string),
              oldType: "longtext")
              .Annotation("MySql:CharSet", "utf8mb4")
              .OldAnnotation("MySql:CharSet", "utf8mb4");
      }
  }
