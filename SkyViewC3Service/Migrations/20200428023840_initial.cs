using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyViewC3Service.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    GradeID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.GradeID);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    GradeID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "GradeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradeInitialPermission",
                columns: table => new
                {
                    GradeID = table.Column<string>(nullable: false),
                    PermissionID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeInitialPermission", x => new { x.GradeID, x.PermissionID });
                    table.ForeignKey(
                        name: "FK_GradeInitialPermission_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "GradeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeInitialPermission_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAction",
                columns: table => new
                {
                    UserActionID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    Subsection = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAction", x => x.UserActionID);
                    table.ForeignKey(
                        name: "FK_UserAction_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserHistory",
                columns: table => new
                {
                    UserHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    GradeID = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    AddByUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistory", x => x.UserHistoryID);
                    table.ForeignKey(
                        name: "FK_UserHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserHistory_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "GradeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserHistory_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    PermissionID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => new { x.UserID, x.PermissionID });
                    table.ForeignKey(
                        name: "FK_UserPermission_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermission_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionHistory",
                columns: table => new
                {
                    UserPermissionHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: true),
                    PermissionID = table.Column<string>(nullable: true),
                    Action = table.Column<string>(maxLength: 10, nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionHistory", x => x.UserPermissionHistoryID);
                    table.ForeignKey(
                        name: "FK_UserPermissionHistory_User_AddByUserID",
                        column: x => x.AddByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPermissionHistory_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPermissionHistory_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeInitialPermission_PermissionID",
                table: "GradeInitialPermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_User_GradeID",
                table: "User",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAction_UserID",
                table: "UserAction",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistory_AddByUserID",
                table: "UserHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistory_GradeID",
                table: "UserHistory",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistory_UserID",
                table: "UserHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_PermissionID",
                table: "UserPermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionHistory_AddByUserID",
                table: "UserPermissionHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionHistory_PermissionID",
                table: "UserPermissionHistory",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionHistory_UserID",
                table: "UserPermissionHistory",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeInitialPermission");

            migrationBuilder.DropTable(
                name: "UserAction");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "UserPermissionHistory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Grade");
        }
    }
}
