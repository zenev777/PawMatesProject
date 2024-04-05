using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class DelName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Pet's status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Posts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Post Name");
        }
    }
}
