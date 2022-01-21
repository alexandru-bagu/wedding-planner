using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
    public partial class adduniquetableinvitee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppTableInvitees_TableId",
                table: "AppTableInvitees");

            migrationBuilder.CreateIndex(
                name: "IX_AppTableInvitees_TableId_InviteeId",
                table: "AppTableInvitees",
                columns: new[] { "TableId", "InviteeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppTableInvitees_TableId_InviteeId",
                table: "AppTableInvitees");

            migrationBuilder.CreateIndex(
                name: "IX_AppTableInvitees_TableId",
                table: "AppTableInvitees",
                column: "TableId");
        }
    }
}
