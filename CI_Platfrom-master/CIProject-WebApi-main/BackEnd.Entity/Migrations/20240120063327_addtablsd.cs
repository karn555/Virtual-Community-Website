using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Entity.Migrations
{
    public partial class addtablsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MissionTheme",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MissionTheme",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "MissionTheme",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MissionSkill",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MissionSkill",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "MissionSkill",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MissionTheme");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MissionTheme");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "MissionTheme");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MissionSkill");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MissionSkill");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "MissionSkill");
        }
    }
}
