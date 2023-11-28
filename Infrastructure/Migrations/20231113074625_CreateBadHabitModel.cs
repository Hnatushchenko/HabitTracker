#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateBadHabitModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BadHabit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadHabit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BadHabitOccurrence",
                columns: table => new
                {
                    OccurrenceDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BadHabitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadHabitOccurrence", x => new { x.BadHabitId, x.OccurrenceDate });
                    table.ForeignKey(
                        name: "FK_BadHabitOccurrence_BadHabit_BadHabitId",
                        column: x => x.BadHabitId,
                        principalTable: "BadHabit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadHabitOccurrence");

            migrationBuilder.DropTable(
                name: "BadHabit");
        }
    }
}
