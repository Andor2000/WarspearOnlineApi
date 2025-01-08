using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarspearOnlineApi.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wo_ClassFraction");

            migrationBuilder.AddColumn<int>(
                name: "rf_FractionID",
                table: "wo_Class",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "wo_UserGroup",
                columns: table => new
                {
                    UserGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rf_UserID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    rf_GroupID = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_UserGroup", x => x.UserGroupID);
                    table.ForeignKey(
                        name: "FK_wo_UserGroup_wo_Group_rf_GroupID",
                        column: x => x.rf_GroupID,
                        principalTable: "wo_Group",
                        principalColumn: "GroupID");
                    table.ForeignKey(
                        name: "FK_wo_UserGroup_wo_User_rf_UserID",
                        column: x => x.rf_UserID,
                        principalTable: "wo_User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_wo_Class_rf_FractionID",
                table: "wo_Class",
                column: "rf_FractionID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_UserGroup_rf_GroupID",
                table: "wo_UserGroup",
                column: "rf_GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_UserGroup_rf_UserID",
                table: "wo_UserGroup",
                column: "rf_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_wo_Class_wo_Fraction_rf_FractionID",
                table: "wo_Class",
                column: "rf_FractionID",
                principalTable: "wo_Fraction",
                principalColumn: "FractionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wo_Class_wo_Fraction_rf_FractionID",
                table: "wo_Class");

            migrationBuilder.DropTable(
                name: "wo_UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_wo_Class_rf_FractionID",
                table: "wo_Class");

            migrationBuilder.DropColumn(
                name: "rf_FractionID",
                table: "wo_Class");

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

            migrationBuilder.CreateIndex(
                name: "IX_wo_ClassFraction_rf_ClassID",
                table: "wo_ClassFraction",
                column: "rf_ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_wo_ClassFraction_rf_FractionID",
                table: "wo_ClassFraction",
                column: "rf_FractionID");
        }
    }
}
