using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
    public partial class AddPrintInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeasurementUnit",
                table: "AppInvitationDesigns",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "PaperDpi",
                table: "AppInvitationDesigns",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PaperHeight",
                table: "AppInvitationDesigns",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PaperWidth",
                table: "AppInvitationDesigns",
                type: "double",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasurementUnit",
                table: "AppInvitationDesigns");

            migrationBuilder.DropColumn(
                name: "PaperDpi",
                table: "AppInvitationDesigns");

            migrationBuilder.DropColumn(
                name: "PaperHeight",
                table: "AppInvitationDesigns");

            migrationBuilder.DropColumn(
                name: "PaperWidth",
                table: "AppInvitationDesigns");
        }
    }
}
