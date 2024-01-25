using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class ExtendMaxAndMinSalaryJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "JobsSalaryType",
                newName: "MinAmount");

            migrationBuilder.AddColumn<double>(
                name: "MaxAmount",
                table: "JobsSalaryType",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxAmount",
                table: "JobsSalaryType");

            migrationBuilder.RenameColumn(
                name: "MinAmount",
                table: "JobsSalaryType",
                newName: "Amount");
        }
    }
}
