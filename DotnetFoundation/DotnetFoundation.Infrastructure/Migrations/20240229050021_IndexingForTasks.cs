using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetFoundation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IndexingForTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tasks_AssignedTo",
                table: "tasks",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_Status",
                table: "tasks",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tasks_AssignedTo",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_Status",
                table: "tasks");
        }
    }
}
