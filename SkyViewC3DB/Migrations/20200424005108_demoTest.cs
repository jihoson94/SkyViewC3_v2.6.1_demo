using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyViewC3DB.Migrations
{
    public partial class demoTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    BoxID = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsOut = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.BoxID);
                });

            migrationBuilder.CreateTable(
                name: "Racks",
                columns: table => new
                {
                    RackID = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.RackID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Vials",
                columns: table => new
                {
                    VialID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsOut = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vials", x => x.VialID);
                });

            migrationBuilder.CreateTable(
                name: "BoxActions",
                columns: table => new
                {
                    BoxActionID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoxID = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    RackID = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxActions", x => x.BoxActionID);
                    table.ForeignKey(
                        name: "FK_BoxActions_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxActions_Racks_RackID",
                        column: x => x.RackID,
                        principalTable: "Racks",
                        principalColumn: "RackID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxActions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VialActions",
                columns: table => new
                {
                    VialActionID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VialID = table.Column<string>(nullable: true),
                    BoxID = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VialActions", x => x.VialActionID);
                    table.ForeignKey(
                        name: "FK_VialActions_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VialActions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VialActions_Vials_VialID",
                        column: x => x.VialID,
                        principalTable: "Vials",
                        principalColumn: "VialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxActions_BoxID",
                table: "BoxActions",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxActions_UserID",
                table: "BoxActions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxActions_RackID_Position_Time",
                table: "BoxActions",
                columns: new[] { "RackID", "Position", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_VialActions_UserID",
                table: "VialActions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_VialActions_VialID",
                table: "VialActions",
                column: "VialID");

            migrationBuilder.CreateIndex(
                name: "IX_VialActions_BoxID_Position_Time",
                table: "VialActions",
                columns: new[] { "BoxID", "Position", "Time" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxActions");

            migrationBuilder.DropTable(
                name: "VialActions");

            migrationBuilder.DropTable(
                name: "Racks");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vials");
        }
    }
}
