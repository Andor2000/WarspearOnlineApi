using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarspearOnlineApi.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wo_DropPlayer",
                columns: table => new
                {
                    DropPlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rf_DropID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_PlayerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_DropPlayer", x => x.DropPlayerID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Fraction",
                columns: table => new
                {
                    DropPlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Fraction", x => x.DropPlayerID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Group",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Group", x => x.GroupID);
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
                });

            migrationBuilder.CreateTable(
                name: "wo_Guild",
                columns: table => new
                {
                    GuildID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rf_ServerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_FractionID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Guild", x => x.GuildID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Object",
                columns: table => new
                {
                    ObjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rf_ObjectTypeID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Object", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "wo_ObjectType",
                columns: table => new
                {
                    ObjectTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_ObjectType", x => x.ObjectTypeID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Player",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rf_ServerID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_FractionID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Player", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Server",
                columns: table => new
                {
                    ServerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Server", x => x.ServerID);
                });

            migrationBuilder.CreateTable(
                name: "wo_Drop",
                columns: table => new
                {
                    DropID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Drop_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    rf_ObjectID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_GroupID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_Drop", x => x.DropID);
                    table.ForeignKey(
                        name: "FK_wo_Drop_wo_Group_rf_GroupID",
                        column: x => x.rf_GroupID,
                        principalTable: "wo_Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wo_Drop_wo_Object_rf_ObjectID",
                        column: x => x.rf_ObjectID,
                        principalTable: "wo_Object",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wo_Drop_rf_GroupID",
                table: "wo_Drop",
                column: "rf_GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_Drop_rf_ObjectID",
                table: "wo_Drop",
                column: "rf_ObjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wo_Drop");

            migrationBuilder.DropTable(
                name: "wo_DropPlayer");

            migrationBuilder.DropTable(
                name: "wo_Fraction");

            migrationBuilder.DropTable(
                name: "wo_GroupGuild");

            migrationBuilder.DropTable(
                name: "wo_Guild");

            migrationBuilder.DropTable(
                name: "wo_ObjectType");

            migrationBuilder.DropTable(
                name: "wo_Player");

            migrationBuilder.DropTable(
                name: "wo_Server");

            migrationBuilder.DropTable(
                name: "wo_Group");

            migrationBuilder.DropTable(
                name: "wo_Object");
        }
    }
}
