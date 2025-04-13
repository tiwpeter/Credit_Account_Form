using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shippings",
                keyColumn: "shipping_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shippings",
                keyColumn: "shipping_id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "CountryModelCountryId", "ProvinceId", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, null, 1, "123 Sukhumvit", "10110" },
                    { 2, null, 2, "456 Nimman", "50000" }
                });

            migrationBuilder.InsertData(
                table: "Shippings",
                columns: new[] { "shipping_id", "CountryModelCountryId", "ProvinceId", "subDistrict" },
                values: new object[,]
                {
                    { 1, null, 1, "Wattana" },
                    { 2, null, 3, "Shibuya" }
                });
        }
    }
}
