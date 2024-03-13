using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class ownerEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_ApplicationUserId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Pets",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_ApplicationUserId",
                table: "Pets",
                newName: "IX_Pets_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_OwnerId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Pets",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                newName: "IX_Pets_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_ApplicationUserId",
                table: "Pets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
