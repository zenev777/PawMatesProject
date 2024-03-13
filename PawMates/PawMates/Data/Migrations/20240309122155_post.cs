using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Pets");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Post identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Post Name"),
                    Description = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false, comment: "Post Description"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Post ImageUrl"),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatorId",
                table: "Posts",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Owner identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "User's adress name"),
                    Country = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false, comment: "User's countrty name"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User's first name"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "User's gender"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User's last name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }
    }
}
