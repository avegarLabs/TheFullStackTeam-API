using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class RelationShipCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobContratType_Jobs_JobId",
                table: "JobContratType");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobType_Jobs_JobId",
                table: "JobJobType");

            migrationBuilder.DropForeignKey(
                name: "FK_JobLanguague_Jobs_JobId",
                table: "JobLanguague");

            migrationBuilder.DropForeignKey(
                name: "FK_JobResponsabilities_Jobs_JobId",
                table: "JobResponsabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkills_Jobs_JobId",
                table: "JobSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalContratType_Professionals_ProfessionalId",
                table: "ProfessionalContratType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalJobType_Professionals_ProfessionalId",
                table: "ProfessionalJobType");

            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_Countries_CountryId",
                table: "Professionals");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalSalaryType_Professionals_ProfessionalId",
                table: "ProfessionalSalaryType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalServices_Professionals_ProfessionalId",
                table: "ProfessionalServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalSkills_Professionals_ProfessionalId",
                table: "ProfessionalSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_JobContratType_Jobs_JobId",
                table: "JobContratType",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobType_Jobs_JobId",
                table: "JobJobType",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobLanguague_Jobs_JobId",
                table: "JobLanguague",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobResponsabilities_Jobs_JobId",
                table: "JobResponsabilities",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkills_Jobs_JobId",
                table: "JobSkills",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalContratType_Professionals_ProfessionalId",
                table: "ProfessionalContratType",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalJobType_Professionals_ProfessionalId",
                table: "ProfessionalJobType",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_Countries_CountryId",
                table: "Professionals",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalSalaryType_Professionals_ProfessionalId",
                table: "ProfessionalSalaryType",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalServices_Professionals_ProfessionalId",
                table: "ProfessionalServices",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalSkills_Professionals_ProfessionalId",
                table: "ProfessionalSkills",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobContratType_Jobs_JobId",
                table: "JobContratType");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobType_Jobs_JobId",
                table: "JobJobType");

            migrationBuilder.DropForeignKey(
                name: "FK_JobLanguague_Jobs_JobId",
                table: "JobLanguague");

            migrationBuilder.DropForeignKey(
                name: "FK_JobResponsabilities_Jobs_JobId",
                table: "JobResponsabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkills_Jobs_JobId",
                table: "JobSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalContratType_Professionals_ProfessionalId",
                table: "ProfessionalContratType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalJobType_Professionals_ProfessionalId",
                table: "ProfessionalJobType");

            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_Countries_CountryId",
                table: "Professionals");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalSalaryType_Professionals_ProfessionalId",
                table: "ProfessionalSalaryType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalServices_Professionals_ProfessionalId",
                table: "ProfessionalServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalSkills_Professionals_ProfessionalId",
                table: "ProfessionalSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_JobContratType_Jobs_JobId",
                table: "JobContratType",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobType_Jobs_JobId",
                table: "JobJobType",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobLanguague_Jobs_JobId",
                table: "JobLanguague",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobResponsabilities_Jobs_JobId",
                table: "JobResponsabilities",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkills_Jobs_JobId",
                table: "JobSkills",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalContratType_Professionals_ProfessionalId",
                table: "ProfessionalContratType",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalJobType_Professionals_ProfessionalId",
                table: "ProfessionalJobType",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_Countries_CountryId",
                table: "Professionals",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalSalaryType_Professionals_ProfessionalId",
                table: "ProfessionalSalaryType",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalServices_Professionals_ProfessionalId",
                table: "ProfessionalServices",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalSkills_Professionals_ProfessionalId",
                table: "ProfessionalSkills",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
