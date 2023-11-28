#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParentChildRelationshipBetweenToDoItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ToDoItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ParentId",
                table: "ToDoItems",
                column: "ParentId");

            migrationBuilder.AddCheckConstraint(
                name: "DefaultStartTime",
                table: "Habits",
                sql: "DefaultStartTime <= DefaultEndTime");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoItems_ParentId",
                table: "ToDoItems",
                column: "ParentId",
                principalTable: "ToDoItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoItems_ParentId",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ParentId",
                table: "ToDoItems");

            migrationBuilder.DropCheckConstraint(
                name: "DefaultStartTime",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ToDoItems");
        }
    }
}
