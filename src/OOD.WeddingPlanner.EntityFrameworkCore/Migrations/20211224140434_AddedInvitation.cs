﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD.WeddingPlanner.Migrations
{
  public partial class AddedInvitation : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "AppInvitations",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            TenantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
            WeddingId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
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
            table.PrimaryKey("PK_AppInvitations", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "AppInvitations");
    }
  }
}
