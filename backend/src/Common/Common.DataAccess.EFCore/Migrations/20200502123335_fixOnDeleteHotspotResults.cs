using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class fixOnDeleteHotspotResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WonCodes_HotspotResults_HotspotResultId",
                schema: "lottery",
                table: "WonCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_WonCodes_HotspotResults_HotspotResultId",
                schema: "lottery",
                table: "WonCodes",
                column: "HotspotResultId",
                principalSchema: "lottery",
                principalTable: "HotspotResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WonCodes_HotspotResults_HotspotResultId",
                schema: "lottery",
                table: "WonCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_WonCodes_HotspotResults_HotspotResultId",
                schema: "lottery",
                table: "WonCodes",
                column: "HotspotResultId",
                principalSchema: "lottery",
                principalTable: "HotspotResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
