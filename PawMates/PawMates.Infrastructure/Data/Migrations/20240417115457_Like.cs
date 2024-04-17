using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class Like : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikePost_AspNetUsers_UserId",
                table: "LikePost");

            migrationBuilder.DropForeignKey(
                name: "FK_LikePost_Posts_PostId",
                table: "LikePost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikePost",
                table: "LikePost");

            migrationBuilder.RenameTable(
                name: "LikePost",
                newName: "LikePosts");

            migrationBuilder.RenameIndex(
                name: "IX_LikePost_UserId",
                table: "LikePosts",
                newName: "IX_LikePosts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikePosts",
                table: "LikePosts",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_AspNetUsers_UserId",
                table: "LikePosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_AspNetUsers_UserId",
                table: "LikePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikePosts",
                table: "LikePosts");

            migrationBuilder.RenameTable(
                name: "LikePosts",
                newName: "LikePost");

            migrationBuilder.RenameIndex(
                name: "IX_LikePosts_UserId",
                table: "LikePost",
                newName: "IX_LikePost_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikePost",
                table: "LikePost",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LikePost_AspNetUsers_UserId",
                table: "LikePost",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikePost_Posts_PostId",
                table: "LikePost",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
