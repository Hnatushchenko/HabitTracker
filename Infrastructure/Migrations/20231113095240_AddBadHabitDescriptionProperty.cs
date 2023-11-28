#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBadHabitDescriptionProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BadHabits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BadHabits");
        }
    }
}
