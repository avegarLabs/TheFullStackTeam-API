using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class DefineSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobsSalaryType_SalaryTypes_SalaryTypeId",
                table: "JobsSalaryType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalSalaryType_SalaryTypes_SalaryTypeId",
                table: "ProfessionalSalaryType");

            migrationBuilder.DropTable(
                name: "SalaryTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProfessionalSalaryType_SalaryTypeId",
                table: "ProfessionalSalaryType");

            migrationBuilder.DropIndex(
                name: "IX_JobsSalaryType_SalaryTypeId",
                table: "JobsSalaryType");

            migrationBuilder.DropColumn(
                name: "SalaryTypeId",
                table: "ProfessionalSalaryType");

            migrationBuilder.DropColumn(
                name: "SalaryTypeId",
                table: "JobsSalaryType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SalaryTypeId",
                table: "ProfessionalSalaryType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SalaryTypeId",
                table: "JobsSalaryType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SalaryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalSalaryType_SalaryTypeId",
                table: "ProfessionalSalaryType",
                column: "SalaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobsSalaryType_SalaryTypeId",
                table: "JobsSalaryType",
                column: "SalaryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobsSalaryType_SalaryTypes_SalaryTypeId",
                table: "JobsSalaryType",
                column: "SalaryTypeId",
                principalTable: "SalaryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalSalaryType_SalaryTypes_SalaryTypeId",
                table: "ProfessionalSalaryType",
                column: "SalaryTypeId",
                principalTable: "SalaryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
