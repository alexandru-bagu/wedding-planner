using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
    public partial class addtimezonefixinvitationview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "AppEvents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "AppEvents");
        }
    }
}
