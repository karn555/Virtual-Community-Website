using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Entity.Migrations
{
    public partial class InitialAddTabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "VolunteeringGoals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "VolunteeringGoals");
        }
    }
}
