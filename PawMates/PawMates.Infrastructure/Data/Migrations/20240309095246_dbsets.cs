using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawMates.Data.Migrations
{
    public partial class dbsets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_OrganiserId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipant_AspNetUsers_HelperId",
                table: "EventParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipant_Event_EventId",
                table: "EventParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetType",
                table: "PetType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipant",
                table: "EventParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "PetType",
                newName: "PetTypes");

            migrationBuilder.RenameTable(
                name: "EventParticipant",
                newName: "EventParticipants");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipant_HelperId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_HelperId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_OrganiserId",
                table: "Events",
                newName: "IX_Events_OrganiserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetTypes",
                table: "PetTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants",
                columns: new[] { "EventId", "HelperId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Owner identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User's first name"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User's last name"),
                    Country = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false, comment: "User's countrty name"),
                    Adress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "User's adress name"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "User's gender")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Pet identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "Pet's name"),
                    Age = table.Column<int>(type: "int", maxLength: 100, nullable: false, comment: "Pet's age"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Pet's birthday"),
                    Breed = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Pet's breed"),
                    MainColor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Pet's main color"),
                    SecondaryColor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Pet's second color"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "Pet's gender"),
                    Weight = table.Column<double>(type: "float", maxLength: 100, nullable: false, comment: "Pet's weight"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PetTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_PetTypes_PetTypeId",
                        column: x => x.PetTypeId,
                        principalTable: "PetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ApplicationUserId",
                table: "Pets",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetTypeId",
                table: "Pets",
                column: "PetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_AspNetUsers_HelperId",
                table: "EventParticipants",
                column: "HelperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganiserId",
                table: "Events",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_AspNetUsers_HelperId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganiserId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetTypes",
                table: "PetTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants");

            migrationBuilder.RenameTable(
                name: "PetTypes",
                newName: "PetType");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "EventParticipants",
                newName: "EventParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganiserId",
                table: "Event",
                newName: "IX_Event_OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_HelperId",
                table: "EventParticipant",
                newName: "IX_EventParticipant_HelperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetType",
                table: "PetType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipant",
                table: "EventParticipant",
                columns: new[] { "EventId", "HelperId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_OrganiserId",
                table: "Event",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipant_AspNetUsers_HelperId",
                table: "EventParticipant",
                column: "HelperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipant_Event_EventId",
                table: "EventParticipant",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");
        }
    }
}
