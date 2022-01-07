using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserveIt.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationDtos_ReservationResponseId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationResponseId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationResponseId",
                table: "Reservations");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ReservationResponseId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationResponseId",
                table: "Reservations",
                column: "ReservationResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationDtos_ReservationResponseId",
                table: "Reservations",
                column: "ReservationResponseId",
                principalTable: "ReservationDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
