using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserveIt.Migrations
{
    public partial class BitFlagsForTheWin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectureDevices",
                table: "ConferenceRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectureDevices",
                table: "ConferenceRooms");
        }
    }
}
