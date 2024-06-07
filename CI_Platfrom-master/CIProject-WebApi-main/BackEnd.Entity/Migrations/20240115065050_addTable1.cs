using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Entity.Migrations
{
    public partial class addTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionTitle",
                table: "MissionApplication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MissionTitle",
                table: "MissionApplication",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
