﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFullStackTeam.Persistence.Migrations
{
    public partial class ExtendProjectEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Projects");
        }
    }
}
