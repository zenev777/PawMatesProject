using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class PetImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Pet's image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Pets",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0,
                comment: "Pet's age");
        }
    }
}
