using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class FixJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobLanguague");

            migrationBuilder.AddColumn<Guid>(
                name: "JobsId",
                table: "Languages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValueSql: "1");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_JobsId",
                table: "Languages",
                column: "JobsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Jobs_JobsId",
                table: "Languages",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Jobs_JobsId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_JobsId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "JobsId",
                table: "Languages");

            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Jobs",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "JobLanguague",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguagueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LanguagueLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguagueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLanguague", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobLanguague_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobLanguague_Languages_LanguagueId",
                        column: x => x.LanguagueId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobLanguague_JobId",
                table: "JobLanguague",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLanguague_LanguagueId",
                table: "JobLanguague",
                column: "LanguagueId");
        }
    }
}
