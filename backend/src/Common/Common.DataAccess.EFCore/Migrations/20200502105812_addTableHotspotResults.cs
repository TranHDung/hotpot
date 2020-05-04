using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class addTableHotspotResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lottery");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "starter_core",
                newName: "Users",
                newSchema: "lottery");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "starter_core",
                newName: "UserRoles",
                newSchema: "lottery");

            migrationBuilder.RenameTable(
                name: "UserPhotos",
                schema: "starter_core",
                newName: "UserPhotos",
                newSchema: "lottery");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "starter_core",
                newName: "UserClaims",
                newSchema: "lottery");

            migrationBuilder.RenameTable(
                name: "Settings",
                schema: "starter_core",
                newName: "Settings",
                newSchema: "lottery");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "starter_core",
                newName: "Roles",
                newSchema: "lottery");

            migrationBuilder.CreateTable(
                name: "HotspotResults",
                schema: "lottery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrawNumber = table.Column<string>(nullable: false),
                    DrawDate = table.Column<DateTime>(nullable: false),
                    BlueString = table.Column<string>(nullable: false),
                    YellowString = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotspotResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotspotResults",
                schema: "lottery");

            migrationBuilder.EnsureSchema(
                name: "starter_core");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "lottery",
                newName: "Users",
                newSchema: "starter_core");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "lottery",
                newName: "UserRoles",
                newSchema: "starter_core");

            migrationBuilder.RenameTable(
                name: "UserPhotos",
                schema: "lottery",
                newName: "UserPhotos",
                newSchema: "starter_core");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "lottery",
                newName: "UserClaims",
                newSchema: "starter_core");

            migrationBuilder.RenameTable(
                name: "Settings",
                schema: "lottery",
                newName: "Settings",
                newSchema: "starter_core");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "lottery",
                newName: "Roles",
                newSchema: "starter_core");
        }
    }
}
