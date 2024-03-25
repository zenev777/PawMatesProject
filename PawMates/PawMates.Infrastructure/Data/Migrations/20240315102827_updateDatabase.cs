using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Pet's image",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Pet's image");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "Event description",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "Event description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Pet's image",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Pet's image");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "Event description",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "Event description");
        }
    }
}
