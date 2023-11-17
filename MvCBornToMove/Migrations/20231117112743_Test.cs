using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvCBornToMove.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MoveAverageRating_Id",
                table: "MoveAverageRating");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoveAverageRating",
                table: "MoveAverageRating",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveAverageRating",
                table: "MoveAverageRating");

            migrationBuilder.CreateIndex(
                name: "IX_MoveAverageRating_Id",
                table: "MoveAverageRating",
                column: "Id");
        }
    }
}
