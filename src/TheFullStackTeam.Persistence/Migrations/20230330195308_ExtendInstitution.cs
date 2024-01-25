using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class ExtendInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Institution",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Institution",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institution_CountryId",
                table: "Institution",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Institution_Countries_CountryId",
                table: "Institution",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institution_Countries_CountryId",
                table: "Institution");

            migrationBuilder.DropIndex(
                name: "IX_Institution_CountryId",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Institution");
        }
    }
}
