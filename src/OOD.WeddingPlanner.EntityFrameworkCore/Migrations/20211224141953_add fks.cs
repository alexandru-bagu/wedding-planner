using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
  public partial class addfks : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateIndex(
          name: "IX_AppInvitees_InvitationId",
          table: "AppInvitees",
          column: "InvitationId");

      migrationBuilder.CreateIndex(
          name: "IX_AppInvitations_WeddingId",
          table: "AppInvitations",
          column: "WeddingId");

      migrationBuilder.CreateIndex(
          name: "IX_AppEvents_LocationId",
          table: "AppEvents",
          column: "LocationId");

      migrationBuilder.CreateIndex(
          name: "IX_AppEvents_WeddingId",
          table: "AppEvents",
          column: "WeddingId");

      migrationBuilder.AddForeignKey(
          name: "FK_AppEvents_AppLocations_LocationId",
          table: "AppEvents",
          column: "LocationId",
          principalTable: "AppLocations",
          principalColumn: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_AppEvents_AppWeddings_WeddingId",
          table: "AppEvents",
          column: "WeddingId",
          principalTable: "AppWeddings",
          principalColumn: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_AppInvitations_AppWeddings_WeddingId",
          table: "AppInvitations",
          column: "WeddingId",
          principalTable: "AppWeddings",
          principalColumn: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_AppInvitees_AppInvitations_InvitationId",
          table: "AppInvitees",
          column: "InvitationId",
          principalTable: "AppInvitations",
          principalColumn: "Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_AppEvents_AppLocations_LocationId",
          table: "AppEvents");

      migrationBuilder.DropForeignKey(
          name: "FK_AppEvents_AppWeddings_WeddingId",
          table: "AppEvents");

      migrationBuilder.DropForeignKey(
          name: "FK_AppInvitations_AppWeddings_WeddingId",
          table: "AppInvitations");

      migrationBuilder.DropForeignKey(
          name: "FK_AppInvitees_AppInvitations_InvitationId",
          table: "AppInvitees");

      migrationBuilder.DropIndex(
          name: "IX_AppInvitees_InvitationId",
          table: "AppInvitees");

      migrationBuilder.DropIndex(
          name: "IX_AppInvitations_WeddingId",
          table: "AppInvitations");

      migrationBuilder.DropIndex(
          name: "IX_AppEvents_LocationId",
          table: "AppEvents");

      migrationBuilder.DropIndex(
          name: "IX_AppEvents_WeddingId",
          table: "AppEvents");
    }
  }
}
