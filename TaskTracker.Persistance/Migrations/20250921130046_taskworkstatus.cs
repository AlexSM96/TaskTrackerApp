using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class taskworkstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Executed",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "InWork",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "WorkStatus",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkStatus",
                table: "Tasks");

            migrationBuilder.AddColumn<bool>(
                name: "Executed",
                table: "Tasks",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InWork",
                table: "Tasks",
                type: "boolean",
                nullable: true);
        }
    }
}
