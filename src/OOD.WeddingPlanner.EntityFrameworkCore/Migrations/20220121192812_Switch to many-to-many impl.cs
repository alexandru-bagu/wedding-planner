using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
    public partial class Switchtomanytomanyimpl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvitees_AppTables_TableId",
                table: "AppInvitees");

            migrationBuilder.DropIndex(
                name: "IX_AppInvitees_TableId",
                table: "AppInvitees");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "AppInvitees");

            migrationBuilder.CreateTable(
                name: "AppTableInvitees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    TableId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    InviteeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTableInvitees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTableInvitees_AppInvitees_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "AppInvitees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppTableInvitees_AppTables_TableId",
                        column: x => x.TableId,
                        principalTable: "AppTables",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AppTableInvitees_InviteeId",
                table: "AppTableInvitees",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTableInvitees_TableId",
                table: "AppTableInvitees",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTableInvitees");

            migrationBuilder.AddColumn<Guid>(
                name: "TableId",
                table: "AppInvitees",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AppInvitees_TableId",
                table: "AppInvitees",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvitees_AppTables_TableId",
                table: "AppInvitees",
                column: "TableId",
                principalTable: "AppTables",
                principalColumn: "Id");
        }
    }
}
