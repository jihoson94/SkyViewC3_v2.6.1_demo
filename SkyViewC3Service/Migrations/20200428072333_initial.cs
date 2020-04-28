using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyViewC3Service.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmLog",
                columns: table => new
                {
                    AlarmLogID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')"),
                    AlarmCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmLog", x => x.AlarmLogID);
                });

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
                name: "TankStatusLog",
                columns: table => new
                {
                    TankStatusLogID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')"),
                    TopTemperature = table.Column<double>(nullable: false),
                    BottomTemperature = table.Column<double>(nullable: false),
                    BypassTemperature = table.Column<double>(nullable: false),
                    LN2Level = table.Column<double>(nullable: false),
                    LN2Usage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankStatusLog", x => x.TankStatusLogID);
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
                name: "BottomTempCalibrationHistory",
                columns: table => new
                {
                    BottomTempCalibrationHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')"),
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
                name: "ByPassTempCalibrationHistory",
                columns: table => new
                {
                    ByPassTempCalibrationHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')"),
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
                name: "LN2LevelCalibratoinHistory",
                columns: table => new
                {
                    LN2LevelCalibratoinHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')"),
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
                name: "SystemConfigHistory",
                columns: table => new
                {
                    SystemConfigHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigName = table.Column<string>(nullable: true),
                    ConfigValue = table.Column<string>(nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')")
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
                name: "TankConfigHistory",
                columns: table => new
                {
                    TankConfigHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigName = table.Column<string>(nullable: true),
                    ConfigValue = table.Column<string>(nullable: true),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')")
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

            migrationBuilder.CreateTable(
                name: "TopTempCalibrationHistory",
                columns: table => new
                {
                    TopTempCalibrationHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    AddByUserID = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')"),
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
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')"),
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
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')")
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
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')")
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
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')")
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
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "date('now')")
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
                name: "IX_BottomTempCalibrationHistory_AddByUserID",
                table: "BottomTempCalibrationHistory",
                column: "AddByUserID");

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
                name: "IX_ByPassTempCalibrationHistory_AddByUserID",
                table: "ByPassTempCalibrationHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GradeInitialPermission_PermissionID",
                table: "GradeInitialPermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_LN2LevelCalibratoinHistory_AddByUserID",
                table: "LN2LevelCalibratoinHistory",
                column: "AddByUserID");

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
                name: "IX_SystemConfigHistory_AddByUserID",
                table: "SystemConfigHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TankConfigHistory_AddByUserID",
                table: "TankConfigHistory",
                column: "AddByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TopTempCalibrationHistory_AddByUserID",
                table: "TopTempCalibrationHistory",
                column: "AddByUserID");

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
                name: "AlarmLog");

            migrationBuilder.DropTable(
                name: "BottomTempCalibration");

            migrationBuilder.DropTable(
                name: "BottomTempCalibrationHistory");

            migrationBuilder.DropTable(
                name: "BoxHistory");

            migrationBuilder.DropTable(
                name: "ByPassTempCalibration");

            migrationBuilder.DropTable(
                name: "ByPassTempCalibrationHistory");

            migrationBuilder.DropTable(
                name: "GradeInitialPermission");

            migrationBuilder.DropTable(
                name: "LN2LevelCalibration");

            migrationBuilder.DropTable(
                name: "LN2LevelCalibratoinHistory");

            migrationBuilder.DropTable(
                name: "SpaceOwnership");

            migrationBuilder.DropTable(
                name: "SpaceOwnerShipHistory");

            migrationBuilder.DropTable(
                name: "SystemConfig");

            migrationBuilder.DropTable(
                name: "SystemConfigHistory");

            migrationBuilder.DropTable(
                name: "TankConfig");

            migrationBuilder.DropTable(
                name: "TankConfigHistory");

            migrationBuilder.DropTable(
                name: "TankStatusLog");

            migrationBuilder.DropTable(
                name: "TopTempCalibration");

            migrationBuilder.DropTable(
                name: "TopTempCalibrationHistory");

            migrationBuilder.DropTable(
                name: "UserAction");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "UserPermissionHistory");

            migrationBuilder.DropTable(
                name: "VialHistory");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Vial");

            migrationBuilder.DropTable(
                name: "Grade");

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
