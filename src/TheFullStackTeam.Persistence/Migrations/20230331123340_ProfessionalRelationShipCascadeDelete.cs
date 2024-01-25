using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class ProfessionalRelationShipCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Organizations_OrganizationId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Professionals_ProfessionalId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsSalaryType_Jobs_JobId",
                table: "JobsSalaryType");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillPositions_Position_PositionId",
                table: "SkillPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Organizations_OrganizationId",
                table: "Jobs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Professionals_ProfessionalId",
                table: "Jobs",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSalaryType_Jobs_JobId",
                table: "JobsSalaryType",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillPositions_Position_PositionId",
                table: "SkillPositions",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices",
                column: "ProfessionalSevicesId",
                principalTable: "ProfessionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Organizations_OrganizationId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Professionals_ProfessionalId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsSalaryType_Jobs_JobId",
                table: "JobsSalaryType");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillPositions_Position_PositionId",
                table: "SkillPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Organizations_OrganizationId",
                table: "Jobs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Professionals_ProfessionalId",
                table: "Jobs",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSalaryType_Jobs_JobId",
                table: "JobsSalaryType",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillPositions_Position_PositionId",
                table: "SkillPositions",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices",
                column: "ProfessionalSevicesId",
                principalTable: "ProfessionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
