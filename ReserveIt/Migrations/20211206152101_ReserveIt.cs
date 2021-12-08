using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserveIt.Migrations
{
    public partial class ReserveIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ConferenceRooms_ConferenceRoomId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ConferenceRoomId",
                table: "Reservations",
                newName: "ReservationResponseId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ConferenceRoomId",
                table: "Reservations",
                newName: "IX_Reservations_ReservationResponseId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "ConferenceRooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "ConferenceRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReservationDtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationDtos_ConferenceRooms_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "ConferenceRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ResourceId",
                table: "Reservations",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDtos_ResourceId",
                table: "ReservationDtos",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ConferenceRooms_ResourceId",
                table: "Reservations",
                column: "ResourceId",
                principalTable: "ConferenceRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationDtos_ReservationResponseId",
                table: "Reservations",
                column: "ReservationResponseId",
                principalTable: "ReservationDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ConferenceRooms_ResourceId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationDtos_ReservationResponseId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationDtos");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ResourceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "ConferenceRooms");

            migrationBuilder.RenameColumn(
                name: "ReservationResponseId",
                table: "Reservations",
                newName: "ConferenceRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ReservationResponseId",
                table: "Reservations",
                newName: "IX_Reservations_ConferenceRoomId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "ConferenceRooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ConferenceRooms_ConferenceRoomId",
                table: "Reservations",
                column: "ConferenceRoomId",
                principalTable: "ConferenceRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
