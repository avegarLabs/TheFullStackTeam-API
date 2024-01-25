using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class DefineProfessionalJobandContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobContratType_ContractType_ContractTypeId",
                table: "JobContratType");

            migrationBuilder.DropForeignKey(
                name: "FK_JobJobType_JobTypes_JobTypeId",
                table: "JobJobType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalContratType_ContractType_ContractTypeId",
                table: "ProfessionalContratType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalJobType_JobTypes_JobTypeId",
                table: "ProfessionalJobType");

            migrationBuilder.DropTable(
                name: "ContractType");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProfessionalJobType_JobTypeId",
                table: "ProfessionalJobType");

            migrationBuilder.DropIndex(
                name: "IX_ProfessionalContratType_ContractTypeId",
                table: "ProfessionalContratType");

            migrationBuilder.DropIndex(
                name: "IX_JobJobType_JobTypeId",
                table: "JobJobType");

            migrationBuilder.DropIndex(
                name: "IX_JobContratType_ContractTypeId",
                table: "JobContratType");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "ProfessionalJobType");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "ProfessionalContratType");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "JobJobType");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "JobContratType");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProfessionalJobType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProfessionalContratType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProfessionalJobType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProfessionalContratType");

            migrationBuilder.AddColumn<Guid>(
                name: "JobTypeId",
                table: "ProfessionalJobType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeId",
                table: "ProfessionalContratType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "JobTypeId",
                table: "JobJobType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractTypeId",
                table: "JobContratType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalJobType_JobTypeId",
                table: "ProfessionalJobType",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalContratType_ContractTypeId",
                table: "ProfessionalContratType",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobJobType_JobTypeId",
                table: "JobJobType",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContratType_ContractTypeId",
                table: "JobContratType",
                column: "ContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobContratType_ContractType_ContractTypeId",
                table: "JobContratType",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobJobType_JobTypes_JobTypeId",
                table: "JobJobType",
                column: "JobTypeId",
                principalTable: "JobTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalContratType_ContractType_ContractTypeId",
                table: "ProfessionalContratType",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalJobType_JobTypes_JobTypeId",
                table: "ProfessionalJobType",
                column: "JobTypeId",
                principalTable: "JobTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
