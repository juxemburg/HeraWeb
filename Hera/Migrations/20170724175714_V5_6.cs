using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Migrations
{
    public partial class V5_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteValues");

            migrationBuilder.DropColumn(
                name: "Controller",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "InnerCount",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InnerCount",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RouteValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NotificationId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteValues_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteValues_NotificationId",
                table: "RouteValues",
                column: "NotificationId");
        }
    }
}
