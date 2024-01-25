using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class DefinePosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Position",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Position_OrganizationId",
                table: "Position",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Organizations_OrganizationId",
                table: "Position",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_Organizations_OrganizationId",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Position_OrganizationId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Position");
        }
    }
}
