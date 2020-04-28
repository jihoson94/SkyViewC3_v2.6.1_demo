using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyViewC3Service.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemConfig",
                columns: table => new
                {
                    ConfigName = table.Column<string>(nullable: false),
                    ConfigValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfig", x => x.ConfigName);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigHistory",
                columns: table => new
                {
                    SystemConfigHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigName = table.Column<string>(nullable: true),
                    ConfigValue = table.Column<string>(nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigHistory", x => x.SystemConfigHistoryID);
                    table.ForeignKey(
                        name: "FK_SystemConfigHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TankConfig",
                columns: table => new
                {
                    ConfigName = table.Column<string>(nullable: false),
                    ConfigValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankConfig", x => x.ConfigName);
                });

            migrationBuilder.CreateTable(
                name: "TankConfigHistory",
                columns: table => new
                {
                    TankConfigHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigName = table.Column<string>(nullable: true),
                    ConfigValue = table.Column<string>(nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankConfigHistory", x => x.TankConfigHistoryID);
                    table.ForeignKey(
                        name: "FK_TankConfigHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigHistory_AddByUserID",
                table: "SystemConfigHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TankConfigHistory_AddByUserID",
                table: "TankConfigHistory",
                column: "AddByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemConfig");

            migrationBuilder.DropTable(
                name: "SystemConfigHistory");

            migrationBuilder.DropTable(
                name: "TankConfig");

            migrationBuilder.DropTable(
                name: "TankConfigHistory");
        }
    }
}
