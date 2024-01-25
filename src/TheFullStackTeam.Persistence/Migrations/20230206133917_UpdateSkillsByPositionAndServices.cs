using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class UpdateSkillsByPositionAndServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillPositions_Skills_SkillId",
                table: "SkillPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillProfessionalServices_Skills_SkillId",
                table: "SkillProfessionalServices");

            migrationBuilder.DropIndex(
                name: "IX_SkillProfessionalServices_SkillId",
                table: "SkillProfessionalServices");

            migrationBuilder.DropIndex(
                name: "IX_SkillPositions_SkillId",
                table: "SkillPositions");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "SkillProfessionalServices");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "SkillPositions");

            migrationBuilder.RenameColumn(
                name: "SkillVersion",
                table: "SkillProfessionalServices",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "SkillVersion",
                table: "SkillPositions",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "SkillProfessionalServices",
                newName: "SkillVersion");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "SkillPositions",
                newName: "SkillVersion");

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "SkillProfessionalServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "SkillPositions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SkillProfessionalServices_SkillId",
                table: "SkillProfessionalServices",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillPositions_SkillId",
                table: "SkillPositions",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillPositions_Skills_SkillId",
                table: "SkillPositions",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillProfessionalServices_Skills_SkillId",
                table: "SkillProfessionalServices",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
