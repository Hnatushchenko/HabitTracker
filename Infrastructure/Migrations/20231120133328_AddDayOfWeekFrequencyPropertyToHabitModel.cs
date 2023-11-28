#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDayOfWeekFrequencyPropertyToHabitModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayOfWeekFrequency",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeekFrequency",
                table: "Habits");
        }
    }
}
