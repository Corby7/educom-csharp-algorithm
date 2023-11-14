using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvCBornToMove.Migrations
{
    /// <inheritdoc />
    public partial class IntToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SweatRate",
                table: "Move",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SweatRate",
                table: "Move",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");
        }
    }
}
