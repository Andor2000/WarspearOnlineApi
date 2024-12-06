using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarspearOnlineApi.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wo_AccessLevel",
                columns: table => new
                {
                    AccessLevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessLevelCode = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValue: ""),
                    AccessLevelName = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValue: ""),
                    rf_ParentAccessLevelID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_AccessLevel", x => x.AccessLevelID);
                    table.ForeignKey(
                        name: "FK_wo_AccessLevel_wo_AccessLevel_rf_ParentAccessLevelID",
                        column: x => x.rf_ParentAccessLevelID,
                        principalTable: "wo_AccessLevel",
                        principalColumn: "AccessLevelID");
                });

            migrationBuilder.CreateTable(
                name: "wo_Class",
                columns: table => new
                {
                    ClassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    ClassName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Class", x => x.ClassID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Fraction",
                columns: table => new
                {
                    FractionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FractionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    FractionName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Fraction", x => x.FractionID);
                });

            migrationBuilder.CreateTable(
                name: "wo_ObjectType",
                columns: table => new
                {
                    ObjectTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectTypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    ObjectTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_ObjectType", x => x.ObjectTypeID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleCode = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValue: ""),
                    RoleName = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Server",
                columns: table => new
                {
                    ServerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    ServerName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Server", x => x.ServerID);
                });

            migrationBuilder.CreateTable(
                name: "wo_ClassFraction",
                columns: table => new
                {
                    ClassFractionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rf_ClassID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_FractionID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_ClassFraction", x => x.ClassFractionID);
                    table.ForeignKey(
                        name: "FK_wo_ClassFraction_wo_Class_rf_ClassID",
                        column: x => x.rf_ClassID,
                        principalTable: "wo_Class",
                        principalColumn: "ClassID");
                    table.ForeignKey(
                        name: "FK_wo_ClassFraction_wo_Fraction_rf_FractionID",
                        column: x => x.rf_FractionID,
                        principalTable: "wo_Fraction",
                        principalColumn: "FractionID");
                });

            migrationBuilder.CreateTable(
                name: "wo_Object",
                columns: table => new
                {
                    ObjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    ObjectName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, defaultValue: ""),
                    Image = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false, defaultValue: ""),
                    rf_ObjectTypeID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Object", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_wo_Object_wo_ObjectType_rf_ObjectTypeID",
                        column: x => x.rf_ObjectTypeID,
                        principalTable: "wo_ObjectType",
                        principalColumn: "ObjectTypeID");
                });

            migrationBuilder.CreateTable(
                name: "wo_AccessLevelRole",
                columns: table => new
                {
                    AccessLevelRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rf_AccessLevelID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_RoleID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_AccessLevelRole", x => x.AccessLevelRoleID);
                    table.ForeignKey(
                        name: "FK_wo_AccessLevelRole_wo_AccessLevel_rf_AccessLevelID",
                        column: x => x.rf_AccessLevelID,
                        principalTable: "wo_AccessLevel",
                        principalColumn: "AccessLevelID");
                    table.ForeignKey(
                        name: "FK_wo_AccessLevelRole_wo_Role_rf_RoleID",
                        column: x => x.rf_RoleID,
                        principalTable: "wo_Role",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "wo_Group",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    rf_ServerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_FractionID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Group", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_wo_Group_wo_Fraction_rf_FractionID",
                        column: x => x.rf_FractionID,
                        principalTable: "wo_Fraction",
                        principalColumn: "FractionID");
                    table.ForeignKey(
                        name: "FK_wo_Group_wo_Server_rf_ServerID",
                        column: x => x.rf_ServerID,
                        principalTable: "wo_Server",
                        principalColumn: "ServerID");
                });

            migrationBuilder.CreateTable(
                name: "wo_Guild",
                columns: table => new
                {
                    GuildID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: ""),
                    rf_ServerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_FractionID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Guild", x => x.GuildID);
                    table.ForeignKey(
                        name: "FK_wo_Guild_wo_Fraction_rf_FractionID",
                        column: x => x.rf_FractionID,
                        principalTable: "wo_Fraction",
                        principalColumn: "FractionID");
                    table.ForeignKey(
                        name: "FK_wo_Guild_wo_Server_rf_ServerID",
                        column: x => x.rf_ServerID,
                        principalTable: "wo_Server",
                        principalColumn: "ServerID");
                });

            migrationBuilder.CreateTable(
                name: "wo_Player",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nick = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    rf_ServerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_FractionID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_ClassID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Player", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_wo_Player_wo_Class_rf_ClassID",
                        column: x => x.rf_ClassID,
                        principalTable: "wo_Class",
                        principalColumn: "ClassID");
                    table.ForeignKey(
                        name: "FK_wo_Player_wo_Fraction_rf_FractionID",
                        column: x => x.rf_FractionID,
                        principalTable: "wo_Fraction",
                        principalColumn: "FractionID");
                    table.ForeignKey(
                        name: "FK_wo_Player_wo_Server_rf_ServerID",
                        column: x => x.rf_ServerID,
                        principalTable: "wo_Server",
                        principalColumn: "ServerID");
                });

            migrationBuilder.CreateTable(
                name: "wo_Drop",
                columns: table => new
                {
                    DropID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Drop_Date = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Price = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_ObjectID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_GroupID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_ServerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_FractionID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Drop", x => x.DropID);
                    table.ForeignKey(
                        name: "FK_wo_Drop_wo_Fraction_rf_FractionID",
                        column: x => x.rf_FractionID,
                        principalTable: "wo_Fraction",
                        principalColumn: "FractionID");
                    table.ForeignKey(
                        name: "FK_wo_Drop_wo_Group_rf_GroupID",
                        column: x => x.rf_GroupID,
                        principalTable: "wo_Group",
                        principalColumn: "GroupID");
                    table.ForeignKey(
                        name: "FK_wo_Drop_wo_Object_rf_ObjectID",
                        column: x => x.rf_ObjectID,
                        principalTable: "wo_Object",
                        principalColumn: "ObjectID");
                    table.ForeignKey(
                        name: "FK_wo_Drop_wo_Server_rf_ServerID",
                        column: x => x.rf_ServerID,
                        principalTable: "wo_Server",
                        principalColumn: "ServerID");
                });

            migrationBuilder.CreateTable(
                name: "wo_GroupGuild",
                columns: table => new
                {
                    GroupGuildID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rf_GroupID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_GuildID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_GroupGuild", x => x.GroupGuildID);
                    table.ForeignKey(
                        name: "FK_wo_GroupGuild_wo_Group_rf_GroupID",
                        column: x => x.rf_GroupID,
                        principalTable: "wo_Group",
                        principalColumn: "GroupID");
                    table.ForeignKey(
                        name: "FK_wo_GroupGuild_wo_Guild_rf_GuildID",
                        column: x => x.rf_GuildID,
                        principalTable: "wo_Guild",
                        principalColumn: "GuildID");
                });

            migrationBuilder.CreateTable(
                name: "wo_DropPlayer",
                columns: table => new
                {
                    DropPlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Part = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    rf_DropID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_PlayerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_DropPlayer", x => x.DropPlayerID);
                    table.ForeignKey(
                        name: "FK_wo_DropPlayer_wo_Drop_rf_DropID",
                        column: x => x.rf_DropID,
                        principalTable: "wo_Drop",
                        principalColumn: "DropID");
                    table.ForeignKey(
                        name: "FK_wo_DropPlayer_wo_Player_rf_PlayerID",
                        column: x => x.rf_PlayerID,
                        principalTable: "wo_Player",
                        principalColumn: "PlayerID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_wo_AccessLevel_rf_ParentAccessLevelID",
                table: "wo_AccessLevel",
                column: "rf_ParentAccessLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_AccessLevelRole_rf_AccessLevelID",
                table: "wo_AccessLevelRole",
                column: "rf_AccessLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_AccessLevelRole_rf_RoleID",
                table: "wo_AccessLevelRole",
                column: "rf_RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_ClassFraction_rf_ClassID",
                table: "wo_ClassFraction",
                column: "rf_ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_ClassFraction_rf_FractionID",
                table: "wo_ClassFraction",
                column: "rf_FractionID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Drop_rf_FractionID",
                table: "wo_Drop",
                column: "rf_FractionID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Drop_rf_GroupID",
                table: "wo_Drop",
                column: "rf_GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Drop_rf_ObjectID",
                table: "wo_Drop",
                column: "rf_ObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Drop_rf_ServerID",
                table: "wo_Drop",
                column: "rf_ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_DropPlayer_rf_DropID",
                table: "wo_DropPlayer",
                column: "rf_DropID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_DropPlayer_rf_PlayerID",
                table: "wo_DropPlayer",
                column: "rf_PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Group_rf_FractionID",
                table: "wo_Group",
                column: "rf_FractionID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Group_rf_ServerID",
                table: "wo_Group",
                column: "rf_ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_GroupGuild_rf_GroupID",
                table: "wo_GroupGuild",
                column: "rf_GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_GroupGuild_rf_GuildID",
                table: "wo_GroupGuild",
                column: "rf_GuildID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Guild_rf_FractionID",
                table: "wo_Guild",
                column: "rf_FractionID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Guild_rf_ServerID",
                table: "wo_Guild",
                column: "rf_ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Object_rf_ObjectTypeID",
                table: "wo_Object",
                column: "rf_ObjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Player_Nick",
                table: "wo_Player",
                column: "Nick");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Player_rf_ClassID",
                table: "wo_Player",
                column: "rf_ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Player_rf_FractionID",
                table: "wo_Player",
                column: "rf_FractionID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Player_rf_ServerID",
                table: "wo_Player",
                column: "rf_ServerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wo_AccessLevelRole");

            migrationBuilder.DropTable(
                name: "wo_ClassFraction");

            migrationBuilder.DropTable(
                name: "wo_DropPlayer");

            migrationBuilder.DropTable(
                name: "wo_GroupGuild");

            migrationBuilder.DropTable(
                name: "wo_AccessLevel");

            migrationBuilder.DropTable(
                name: "wo_Role");

            migrationBuilder.DropTable(
                name: "wo_Drop");

            migrationBuilder.DropTable(
                name: "wo_Player");

            migrationBuilder.DropTable(
                name: "wo_Guild");

            migrationBuilder.DropTable(
                name: "wo_Group");

            migrationBuilder.DropTable(
                name: "wo_Object");

            migrationBuilder.DropTable(
                name: "wo_Class");

            migrationBuilder.DropTable(
                name: "wo_Fraction");

            migrationBuilder.DropTable(
                name: "wo_Server");

            migrationBuilder.DropTable(
                name: "wo_ObjectType");
        }
    }
}
