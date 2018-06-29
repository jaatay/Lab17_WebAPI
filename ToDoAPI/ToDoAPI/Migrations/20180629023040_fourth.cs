using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoAPI.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoListID",
                table: "ToDoItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoListID",
                table: "ToDoItems",
                column: "ToDoListID");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListID",
                table: "ToDoItems",
                column: "ToDoListID",
                principalTable: "ToDoLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListID",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ToDoListID",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "ToDoListID",
                table: "ToDoItems");
        }
    }
}
