using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Entity.Migrations
{
    public partial class updatecol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MissionTheme",
                table: "Missions",
                newName: "MissionVideo");

            migrationBuilder.RenameColumn(
                name: "MissionSkill",
                table: "Missions",
                newName: "MissionType");

            migrationBuilder.AlterColumn<int>(
                name: "TotalSheets",
                table: "Missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDeadLine",
                table: "Missions",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<string>(
                name: "MissionSkillId",
                table: "Missions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MissionThemeId",
                table: "Missions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionSkillId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionThemeId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "MissionVideo",
                table: "Missions",
                newName: "MissionTheme");

            migrationBuilder.RenameColumn(
                name: "MissionType",
                table: "Missions",
                newName: "MissionSkill");

            migrationBuilder.AlterColumn<int>(
                name: "TotalSheets",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDeadLine",
                table: "Missions",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
