using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeographyId",
                table: "Provinces",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_GeographyId",
                table: "Provinces",
                column: "GeographyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_GeographyModel_GeographyId",
                table: "Provinces",
                column: "GeographyId",
                principalTable: "GeographyModel",
                principalColumn: "GeographyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_GeographyModel_GeographyId",
                table: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Provinces_GeographyId",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "GeographyId",
                table: "Provinces");
        }
    }
}
