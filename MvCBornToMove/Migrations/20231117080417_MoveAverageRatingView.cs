using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvCBornToMove.Migrations
{
    /// <inheritdoc />
    public partial class MoveAverageRatingView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW MoveAverageRating AS
            SELECT
                m.Id,
                m.Name,
                AVG(CAST(mr.Rating AS FLOAT)) AS AverageRating,
                AVG(CAST(mr.Intensity AS FLOAT)) AS AverageIntensity
            FROM
                Move m
            INNER JOIN
                MoveRating mr ON m.Id = mr.MoveId
            GROUP BY
                m.Id, m.Name;");




        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view MoveAverageRating;");
        }
    }
}
