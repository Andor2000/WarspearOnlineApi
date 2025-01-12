using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarspearOnlineApi.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wo_DropStatus",
                columns: table => new
                {
                    DropStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DropStatusCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    DropStatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_DropStatus", x => x.DropStatusID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wo_DropStatus");
        }
    }
}
