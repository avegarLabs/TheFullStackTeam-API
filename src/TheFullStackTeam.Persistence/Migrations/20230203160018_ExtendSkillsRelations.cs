using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class ExtendSkillsRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalServicesId",
                table: "SkillProfessionalServices");

            migrationBuilder.RenameColumn(
                name: "ProfessionalServicesId",
                table: "SkillProfessionalServices",
                newName: "ProfessionalSevicesId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillProfessionalServices_ProfessionalServicesId",
                table: "SkillProfessionalServices",
                newName: "IX_SkillProfessionalServices_ProfessionalSevicesId");

            migrationBuilder.CreateTable(
                name: "SkillPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillPositions_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkillPositions_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillPositions_PositionId",
                table: "SkillPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillPositions_SkillId",
                table: "SkillPositions",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices",
                column: "ProfessionalSevicesId",
                principalTable: "ProfessionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices");

            migrationBuilder.DropTable(
                name: "SkillPositions");

            migrationBuilder.RenameColumn(
                name: "ProfessionalSevicesId",
                table: "SkillProfessionalServices",
                newName: "ProfessionalServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices",
                newName: "IX_SkillProfessionalServices_ProfessionalServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalServicesId",
                table: "SkillProfessionalServices",
                column: "ProfessionalServicesId",
                principalTable: "ProfessionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
