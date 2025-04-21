using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class FixDeleteBehavior2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ThaiProvince_ThaiProvinceId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "ThaiProvince");

            migrationBuilder.RenameColumn(
                name: "ThaiProvinceId",
                table: "Addresses",
                newName: "CountryModelCountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ThaiProvinceId",
                table: "Addresses",
                newName: "IX_Addresses_CountryModelCountryId");

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AmphureModel",
                columns: table => new
                {
                    AmphureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmphureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceModelProvinceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmphureModel", x => x.AmphureId);
                    table.ForeignKey(
                        name: "FK_AmphureModel_Provinces_ProvinceModelProvinceId",
                        column: x => x.ProvinceModelProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmphureModel_ProvinceModelProvinceId",
                table: "AmphureModel",
                column: "ProvinceModelProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_CountryModel_CountryModelCountryId",
                table: "Addresses",
                column: "CountryModelCountryId",
                principalTable: "CountryModel",
                principalColumn: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_CountryModel_CountryModelCountryId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "AmphureModel");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "CountryModelCountryId",
                table: "Addresses",
                newName: "ThaiProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CountryModelCountryId",
                table: "Addresses",
                newName: "IX_Addresses_ThaiProvinceId");

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "Addresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ThaiProvince",
                columns: table => new
                {
                    ThaiProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    GeographyId = table.Column<int>(type: "int", nullable: false),
                    ThaiProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThaiProvince", x => x.ThaiProvinceId);
                    table.ForeignKey(
                        name: "FK_ThaiProvince_CountryModel_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryModel",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThaiProvince_Geography_GeographyId",
                        column: x => x.GeographyId,
                        principalTable: "Geography",
                        principalColumn: "GeographyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThaiProvince_CountryId",
                table: "ThaiProvince",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ThaiProvince_GeographyId",
                table: "ThaiProvince",
                column: "GeographyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_ThaiProvince_ThaiProvinceId",
                table: "Addresses",
                column: "ThaiProvinceId",
                principalTable: "ThaiProvince",
                principalColumn: "ThaiProvinceId");
        }
    }
}
