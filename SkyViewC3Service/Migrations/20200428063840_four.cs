using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyViewC3Service.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BottomTempCalibration",
                columns: table => new
                {
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BottomTempCalibration", x => new { x.Reference, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "BottomTempCalibrationHistory",
                columns: table => new
                {
                    BottomTempCalibrationHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BottomTempCalibrationHistory", x => x.BottomTempCalibrationHistoryID);
                    table.ForeignKey(
                        name: "FK_BottomTempCalibrationHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ByPassTempCalibration",
                columns: table => new
                {
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ByPassTempCalibration", x => new { x.Reference, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "ByPassTempCalibrationHistory",
                columns: table => new
                {
                    ByPassTempCalibrationHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ByPassTempCalibrationHistory", x => x.ByPassTempCalibrationHistoryID);
                    table.ForeignKey(
                        name: "FK_ByPassTempCalibrationHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LN2LevelCalibration",
                columns: table => new
                {
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LN2LevelCalibration", x => new { x.Reference, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "LN2LevelCalibratoinHistory",
                columns: table => new
                {
                    LN2LevelCalibratoinHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LN2LevelCalibratoinHistory", x => x.LN2LevelCalibratoinHistoryID);
                    table.ForeignKey(
                        name: "FK_LN2LevelCalibratoinHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopTempCalibration",
                columns: table => new
                {
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopTempCalibration", x => new { x.Reference, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "TopTempCalibrationHistory",
                columns: table => new
                {
                    TopTempCalibrationHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopTempCalibrationHistory", x => x.TopTempCalibrationHistoryID);
                    table.ForeignKey(
                        name: "FK_TopTempCalibrationHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BottomTempCalibrationHistory_AddByUserID",
                table: "BottomTempCalibrationHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ByPassTempCalibrationHistory_AddByUserID",
                table: "ByPassTempCalibrationHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LN2LevelCalibratoinHistory_AddByUserID",
                table: "LN2LevelCalibratoinHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TopTempCalibrationHistory_AddByUserID",
                table: "TopTempCalibrationHistory",
                column: "AddByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BottomTempCalibration");

            migrationBuilder.DropTable(
                name: "BottomTempCalibrationHistory");

            migrationBuilder.DropTable(
                name: "ByPassTempCalibration");

            migrationBuilder.DropTable(
                name: "ByPassTempCalibrationHistory");

            migrationBuilder.DropTable(
                name: "LN2LevelCalibration");

            migrationBuilder.DropTable(
                name: "LN2LevelCalibratoinHistory");

            migrationBuilder.DropTable(
                name: "TopTempCalibration");

            migrationBuilder.DropTable(
                name: "TopTempCalibrationHistory");
        }
    }
}
