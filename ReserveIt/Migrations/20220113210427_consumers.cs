using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserveIt.Migrations
{
    public partial class consumers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsMembershipActive = table.Column<bool>(type: "bit", nullable: false),
                    MembershipLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalRewardPoints = table.Column<int>(type: "int", nullable: false),
                    AuthenticatedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumers_Users_AuthenticatedUserId",
                        column: x => x.AuthenticatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AuthenticatedUserId",
                table: "Consumers",
                column: "AuthenticatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumers");
        }
    }
}
