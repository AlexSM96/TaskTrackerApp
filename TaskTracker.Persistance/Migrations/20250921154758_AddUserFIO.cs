using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFIO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FIO",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIO",
                table: "AspNetUsers");
        }
    }
}
