using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
    public partial class renamehtmlparts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvitationNote",
                table: "AppWeddings",
                newName: "InvitationNoteHtml");

            migrationBuilder.RenameColumn(
                name: "InvitationStyle",
                table: "AppWeddings",
                newName: "InvitationHeaderHtml");

            migrationBuilder.AddColumn<string>(
                name: "InvitationFooterHtml",
                table: "AppWeddings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationFooterHtml",
                table: "AppWeddings");

            migrationBuilder.RenameColumn(
                name: "InvitationNoteHtml",
                table: "AppWeddings",
                newName: "InvitationNote");

            migrationBuilder.RenameColumn(
                name: "InvitationHeaderHtml",
                table: "AppWeddings",
                newName: "InvitationStyle");
        }
    }
}
