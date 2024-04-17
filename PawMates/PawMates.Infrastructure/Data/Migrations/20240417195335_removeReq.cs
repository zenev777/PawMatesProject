using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class removeReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Post Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(56)",
                oldMaxLength: 56,
                oldComment: "Post Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Posts",
                type: "nvarchar(56)",
                maxLength: 56,
                nullable: false,
                comment: "Post Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Post Description");
        }
    }
}
