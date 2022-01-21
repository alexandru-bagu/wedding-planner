using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
    public partial class adduniquetableinvitee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppTableInvitees");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppTableInvitees");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppTableInvitees");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppTableInvitees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppTableInvitees");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppTableInvitees");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppTableInvitees",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppTableInvitees",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppTableInvitees",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppTableInvitees",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppTableInvitees",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppTableInvitees",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppTableInvitees",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }
    }
}
