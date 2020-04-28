using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyViewC3Service.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxType",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxType", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "RackType",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RackType", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "VialType",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VialType", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Rack",
                columns: table => new
                {
                    RackID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RackTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rack", x => x.RackID);
                    table.ForeignKey(
                        name: "FK_Rack_RackType_RackTypeName",
                        column: x => x.RackTypeName,
                        principalTable: "RackType",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Box",
                columns: table => new
                {
                    BoxID = table.Column<string>(nullable: false),
                    RackID = table.Column<int>(nullable: false),
                    Slot = table.Column<int>(nullable: false),
                    IsOut = table.Column<bool>(nullable: false),
                    BoxTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.BoxID);
                    table.ForeignKey(
                        name: "FK_Box_BoxType_BoxTypeName",
                        column: x => x.BoxTypeName,
                        principalTable: "BoxType",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Box_Rack_RackID",
                        column: x => x.RackID,
                        principalTable: "Rack",
                        principalColumn: "RackID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpaceOwnership",
                columns: table => new
                {
                    Slot = table.Column<int>(nullable: false),
                    RackID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceOwnership", x => new { x.RackID, x.Slot, x.UserID });
                    table.ForeignKey(
                        name: "FK_SpaceOwnership_Rack_RackID",
                        column: x => x.RackID,
                        principalTable: "Rack",
                        principalColumn: "RackID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceOwnership_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpaceOwnerShipHistory",
                columns: table => new
                {
                    SpaceOwnerShipHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Slot = table.Column<int>(nullable: false),
                    RackID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceOwnerShipHistory", x => x.SpaceOwnerShipHistoryID);
                    table.ForeignKey(
                        name: "FK_SpaceOwnerShipHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpaceOwnerShipHistory_Rack_RackID",
                        column: x => x.RackID,
                        principalTable: "Rack",
                        principalColumn: "RackID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceOwnerShipHistory_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoxHistory",
                columns: table => new
                {
                    BoxHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoxID = table.Column<string>(nullable: true),
                    RackID = table.Column<int>(nullable: false),
                    Slot = table.Column<int>(nullable: false),
                    IsOut = table.Column<bool>(nullable: false),
                    BoxTypeName = table.Column<string>(nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxHistory", x => x.BoxHistoryID);
                    table.ForeignKey(
                        name: "FK_BoxHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxHistory_Box_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Box",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxHistory_BoxType_BoxTypeName",
                        column: x => x.BoxTypeName,
                        principalTable: "BoxType",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxHistory_Rack_RackID",
                        column: x => x.RackID,
                        principalTable: "Rack",
                        principalColumn: "RackID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vial",
                columns: table => new
                {
                    VialID = table.Column<string>(nullable: false),
                    BoxID = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    IsOut = table.Column<bool>(nullable: false),
                    VialTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vial", x => x.VialID);
                    table.ForeignKey(
                        name: "FK_Vial_Box_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Box",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vial_VialType_VialTypeName",
                        column: x => x.VialTypeName,
                        principalTable: "VialType",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VialHistory",
                columns: table => new
                {
                    VialHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VialID = table.Column<string>(nullable: true),
                    BoxID = table.Column<string>(nullable: true),
                    posisiton = table.Column<int>(nullable: false),
                    IsOut = table.Column<bool>(nullable: false),
                    VialTypeName = table.Column<string>(nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VialHistory", x => x.VialHistoryID);
                    table.ForeignKey(
                        name: "FK_VialHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VialHistory_Box_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Box",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VialHistory_Vial_VialID",
                        column: x => x.VialID,
                        principalTable: "Vial",
                        principalColumn: "VialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VialHistory_VialType_VialTypeName",
                        column: x => x.VialTypeName,
                        principalTable: "VialType",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Box_BoxTypeName",
                table: "Box",
                column: "BoxTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Box_RackID",
                table: "Box",
                column: "RackID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxHistory_AddByUserID",
                table: "BoxHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxHistory_BoxID",
                table: "BoxHistory",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxHistory_BoxTypeName",
                table: "BoxHistory",
                column: "BoxTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_BoxHistory_RackID",
                table: "BoxHistory",
                column: "RackID");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_RackTypeName",
                table: "Rack",
                column: "RackTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceOwnership_UserID",
                table: "SpaceOwnership",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceOwnerShipHistory_AddByUserID",
                table: "SpaceOwnerShipHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceOwnerShipHistory_RackID",
                table: "SpaceOwnerShipHistory",
                column: "RackID");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceOwnerShipHistory_UserID",
                table: "SpaceOwnerShipHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Vial_BoxID",
                table: "Vial",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_Vial_VialTypeName",
                table: "Vial",
                column: "VialTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_VialHistory_AddByUserID",
                table: "VialHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_VialHistory_BoxID",
                table: "VialHistory",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_VialHistory_VialID",
                table: "VialHistory",
                column: "VialID");

            migrationBuilder.CreateIndex(
                name: "IX_VialHistory_VialTypeName",
                table: "VialHistory",
                column: "VialTypeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxHistory");

            migrationBuilder.DropTable(
                name: "SpaceOwnership");

            migrationBuilder.DropTable(
                name: "SpaceOwnerShipHistory");

            migrationBuilder.DropTable(
                name: "VialHistory");

            migrationBuilder.DropTable(
                name: "Vial");

            migrationBuilder.DropTable(
                name: "Box");

            migrationBuilder.DropTable(
                name: "VialType");

            migrationBuilder.DropTable(
                name: "BoxType");

            migrationBuilder.DropTable(
                name: "Rack");

            migrationBuilder.DropTable(
                name: "RackType");
        }
    }
}
