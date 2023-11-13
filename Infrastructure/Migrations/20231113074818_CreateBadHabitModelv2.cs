using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateBadHabitModelv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadHabitOccurrence_BadHabit_BadHabitId",
                table: "BadHabitOccurrence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadHabitOccurrence",
                table: "BadHabitOccurrence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadHabit",
                table: "BadHabit");

            migrationBuilder.RenameTable(
                name: "BadHabitOccurrence",
                newName: "BadHabitOccurrences");

            migrationBuilder.RenameTable(
                name: "BadHabit",
                newName: "BadHabits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadHabitOccurrences",
                table: "BadHabitOccurrences",
                columns: new[] { "BadHabitId", "OccurrenceDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadHabits",
                table: "BadHabits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BadHabitOccurrences_BadHabits_BadHabitId",
                table: "BadHabitOccurrences",
                column: "BadHabitId",
                principalTable: "BadHabits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadHabitOccurrences_BadHabits_BadHabitId",
                table: "BadHabitOccurrences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadHabits",
                table: "BadHabits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadHabitOccurrences",
                table: "BadHabitOccurrences");

            migrationBuilder.RenameTable(
                name: "BadHabits",
                newName: "BadHabit");

            migrationBuilder.RenameTable(
                name: "BadHabitOccurrences",
                newName: "BadHabitOccurrence");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadHabit",
                table: "BadHabit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadHabitOccurrence",
                table: "BadHabitOccurrence",
                columns: new[] { "BadHabitId", "OccurrenceDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_BadHabitOccurrence_BadHabit_BadHabitId",
                table: "BadHabitOccurrence",
                column: "BadHabitId",
                principalTable: "BadHabit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
