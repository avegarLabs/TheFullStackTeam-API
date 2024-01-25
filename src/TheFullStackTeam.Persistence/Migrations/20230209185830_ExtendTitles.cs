using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class ExtendTitles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionId",
                table: "Titles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_InstitutionId",
                table: "Titles",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Institution_InstitutionId",
                table: "Titles",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Institution_InstitutionId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Titles_InstitutionId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Titles");
        }
    }
}
