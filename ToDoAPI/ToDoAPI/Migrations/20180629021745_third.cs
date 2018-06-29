using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoAPI.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "ToDoLists");

            migrationBuilder.AddColumn<int>(
                name: "ListID",
                table: "ToDoItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListID",
                table: "ToDoItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemID",
                table: "ToDoLists",
                nullable: true);
        }
    }
}
