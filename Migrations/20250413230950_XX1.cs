using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryModelCountryId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Countries_CountryModelCountryId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_CountryModelCountryId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CountryModelCountryId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountryModelCountryId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "CountryModelCountryId",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryModelCountryId",
                table: "Shippings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryModelCountryId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1,
                column: "CountryModelCountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 2,
                column: "CountryModelCountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 3,
                column: "CountryModelCountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 4,
                column: "CountryModelCountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "shipping_id",
                keyValue: 1,
                column: "CountryModelCountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "shipping_id",
                keyValue: 2,
                column: "CountryModelCountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Shippings",
                keyColumn: "shipping_id",
                keyValue: 3,
                column: "CountryModelCountryId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_CountryModelCountryId",
                table: "Shippings",
                column: "CountryModelCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryModelCountryId",
                table: "Addresses",
                column: "CountryModelCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryModelCountryId",
                table: "Addresses",
                column: "CountryModelCountryId",
                principalTable: "Countries",
                principalColumn: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Countries_CountryModelCountryId",
                table: "Shippings",
                column: "CountryModelCountryId",
                principalTable: "Countries",
                principalColumn: "CountryId");
        }
    }
}
