using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 1,
                column: "GeographyId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 2,
                column: "GeographyId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 3,
                column: "GeographyId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 1,
                column: "GeographyId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 2,
                column: "GeographyId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 3,
                column: "GeographyId",
                value: 0);
        }
    }
}
