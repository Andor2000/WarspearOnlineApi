using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarspearOnlineApi.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration2132112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "wo_User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserLogin",
                table: "wo_User",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldDefaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "wo_User",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "wo_User");

            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "wo_User",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserLogin",
                table: "wo_User",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldDefaultValue: "");
        }
    }
}
