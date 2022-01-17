using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
    public partial class Addplusone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PlusOne",
                table: "AppInvitations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlusOne",
                table: "AppInvitations");
        }
    }
}
