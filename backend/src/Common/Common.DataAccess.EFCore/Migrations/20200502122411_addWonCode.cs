using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class addWonCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YellowString",
                schema: "lottery",
                table: "HotspotResults",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrawNumber",
                schema: "lottery",
                table: "HotspotResults",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BlueString",
                schema: "lottery",
                table: "HotspotResults",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WonCodes",
                schema: "lottery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deficit = table.Column<int>(nullable: false),
                    HotspotResultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WonCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WonCodes_HotspotResults_HotspotResultId",
                        column: x => x.HotspotResultId,
                        principalSchema: "lottery",
                        principalTable: "HotspotResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WonCodes_HotspotResultId",
                schema: "lottery",
                table: "WonCodes",
                column: "HotspotResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WonCodes",
                schema: "lottery");

            migrationBuilder.AlterColumn<string>(
                name: "YellowString",
                schema: "lottery",
                table: "HotspotResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrawNumber",
                schema: "lottery",
                table: "HotspotResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "BlueString",
                schema: "lottery",
                table: "HotspotResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
