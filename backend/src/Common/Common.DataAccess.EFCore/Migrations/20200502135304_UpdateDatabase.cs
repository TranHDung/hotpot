using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YellowString",
                schema: "lottery",
                table: "HotspotResults");

            migrationBuilder.AddColumn<int>(
                name: "CodeId",
                schema: "lottery",
                table: "WonCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "BlueString",
                schema: "lottery",
                table: "HotspotResults",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YellowNumber",
                schema: "lottery",
                table: "HotspotResults",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GroupEmails",
                schema: "lottery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Emails = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "lottery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Codes",
                schema: "lottery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    NumbersString = table.Column<string>(maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    NotAppeareCount = table.Column<int>(nullable: false),
                    ReappeareCount = table.Column<int>(nullable: false),
                    IsAlert = table.Column<bool>(nullable: true),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Codes_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "lottery",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WonCodes_CodeId",
                schema: "lottery",
                table: "WonCodes",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Codes_GroupId",
                schema: "lottery",
                table: "Codes",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_WonCodes_Codes_CodeId",
                schema: "lottery",
                table: "WonCodes",
                column: "CodeId",
                principalSchema: "lottery",
                principalTable: "Codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WonCodes_Codes_CodeId",
                schema: "lottery",
                table: "WonCodes");

            migrationBuilder.DropTable(
                name: "Codes",
                schema: "lottery");

            migrationBuilder.DropTable(
                name: "GroupEmails",
                schema: "lottery");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "lottery");

            migrationBuilder.DropIndex(
                name: "IX_WonCodes_CodeId",
                schema: "lottery",
                table: "WonCodes");

            migrationBuilder.DropColumn(
                name: "CodeId",
                schema: "lottery",
                table: "WonCodes");

            migrationBuilder.DropColumn(
                name: "YellowNumber",
                schema: "lottery",
                table: "HotspotResults");

            migrationBuilder.AlterColumn<string>(
                name: "BlueString",
                schema: "lottery",
                table: "HotspotResults",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "YellowString",
                schema: "lottery",
                table: "HotspotResults",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
