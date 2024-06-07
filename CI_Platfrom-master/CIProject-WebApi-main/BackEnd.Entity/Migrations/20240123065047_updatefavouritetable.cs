using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Entity.Migrations
{
    public partial class updatefavouritetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MissionFavourites",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MissionFavourites",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "MissionFavourites",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MissionFavourites");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MissionFavourites");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "MissionFavourites");
        }
    }
}
