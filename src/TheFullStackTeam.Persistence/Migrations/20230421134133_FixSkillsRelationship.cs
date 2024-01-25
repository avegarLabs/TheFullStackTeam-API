using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class FixSkillsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillOrganizationServices");

            migrationBuilder.DropTable(
                name: "SkillPositions");

            migrationBuilder.DropTable(
                name: "SkillProfessionalServices");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationSevicesId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionalSevicesId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_OrganizationSevicesId",
                table: "Skills",
                column: "OrganizationSevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PositionId",
                table: "Skills",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ProfessionalSevicesId",
                table: "Skills",
                column: "ProfessionalSevicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_OrganizationSevices_OrganizationSevicesId",
                table: "Skills",
                column: "OrganizationSevicesId",
                principalTable: "OrganizationSevices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Position_PositionId",
                table: "Skills",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_ProfessionalServices_ProfessionalSevicesId",
                table: "Skills",
                column: "ProfessionalSevicesId",
                principalTable: "ProfessionalServices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_OrganizationSevices_OrganizationSevicesId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Position_PositionId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_ProfessionalServices_ProfessionalSevicesId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_OrganizationSevicesId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_PositionId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ProfessionalSevicesId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "OrganizationSevicesId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ProfessionalSevicesId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "SkillOrganizationServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationSevicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillOrganizationServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillOrganizationServices_OrganizationSevices_OrganizationSevicesId",
                        column: x => x.OrganizationSevicesId,
                        principalTable: "OrganizationSevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillPositions_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillProfessionalServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessionalSevicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillProfessionalServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillProfessionalServices_ProfessionalServices_ProfessionalSevicesId",
                        column: x => x.ProfessionalSevicesId,
                        principalTable: "ProfessionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillOrganizationServices_OrganizationSevicesId",
                table: "SkillOrganizationServices",
                column: "OrganizationSevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillPositions_PositionId",
                table: "SkillPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillProfessionalServices_ProfessionalSevicesId",
                table: "SkillProfessionalServices",
                column: "ProfessionalSevicesId");
        }
    }
}
