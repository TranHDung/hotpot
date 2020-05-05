using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class addAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "lottery",
                table: "WonCodes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "lottery",
                table: "WonCodes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "lottery",
                table: "HotspotResults",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "lottery",
                table: "HotspotResults",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "lottery",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "lottery",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "lottery",
                table: "GroupEmails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "lottery",
                table: "GroupEmails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "lottery",
                table: "Codes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "lottery",
                table: "Codes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "lottery",
                table: "WonCodes");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "lottery",
                table: "WonCodes");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "lottery",
                table: "HotspotResults");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "lottery",
                table: "HotspotResults");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "lottery",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "lottery",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "lottery",
                table: "GroupEmails");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "lottery",
                table: "GroupEmails");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "lottery",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "lottery",
                table: "Codes");
        }
    }
}
