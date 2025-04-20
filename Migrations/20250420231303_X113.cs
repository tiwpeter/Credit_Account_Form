using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class X113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_ProvinceModel_ProvinceId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceModel_Country_CountryId",
                table: "ProvinceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvinceModel",
                table: "ProvinceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "ProvinceModel",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_ProvinceModel_CountryId",
                table: "Provinces",
                newName: "IX_Provinces_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ProvinceId",
                table: "Addresses",
                newName: "IX_Addresses_ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "AddressId");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1,
                columns: new[] { "Street", "ZipCode" },
                values: new object[] { "123 Sukhumvit Rd", "10110" });

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 2,
                columns: new[] { "Street", "ZipCode" },
                values: new object[] { "456 Nimmanhaemin Rd", "50200" });

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 3,
                columns: new[] { "Street", "ZipCode" },
                values: new object[] { "789 Shibuya", "150-0002" });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Provinces_ProvinceId",
                table: "Addresses",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Country_CountryId",
                table: "Provinces",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Provinces_ProvinceId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Country_CountryId",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "ProvinceModel");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_CountryId",
                table: "ProvinceModel",
                newName: "IX_ProvinceModel_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ProvinceId",
                table: "Address",
                newName: "IX_Address_ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvinceModel",
                table: "ProvinceModel",
                column: "ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 1,
                columns: new[] { "Street", "ZipCode" },
                values: new object[] { "123 Main Rd", "10100" });

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 2,
                columns: new[] { "Street", "ZipCode" },
                values: new object[] { "456 North Ave", "50000" });

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 3,
                columns: new[] { "Street", "ZipCode" },
                values: new object[] { "789 East St", "100-0001" });

            migrationBuilder.AddForeignKey(
                name: "FK_Address_ProvinceModel_ProvinceId",
                table: "Address",
                column: "ProvinceId",
                principalTable: "ProvinceModel",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceModel_Country_CountryId",
                table: "ProvinceModel",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
