using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "GeographyModel",
                columns: table => new
                {
                    GeographyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeographyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeographyModel", x => x.GeographyId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    GeographyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provinces_GeographyModel_GeographyId",
                        column: x => x.GeographyId,
                        principalTable: "GeographyModel",
                        principalColumn: "GeographyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThaiProvinces",
                columns: table => new
                {
                    ThaiProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThaiProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    GeographyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThaiProvinces", x => x.ThaiProvinceId);
                    table.ForeignKey(
                        name: "FK_ThaiProvinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThaiProvinces_GeographyModel_GeographyId",
                        column: x => x.GeographyId,
                        principalTable: "GeographyModel",
                        principalColumn: "GeographyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CountryModelCountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryModelCountryId",
                        column: x => x.CountryModelCountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_Addresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    shipping_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CountryModelCountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.shipping_id);
                    table.ForeignKey(
                        name: "FK_Shippings_Countries_CountryModelCountryId",
                        column: x => x.CountryModelCountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_Shippings_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, "Thailand" },
                    { 2, "Japan" },
                    { 3, "United States" }
                });

            migrationBuilder.InsertData(
                table: "GeographyModel",
                columns: new[] { "GeographyId", "GeographyName" },
                values: new object[,]
                {
                    { 1, "ภาคเหนือ" },
                    { 2, "ภาคกลาง" },
                    { 3, "ภาคตะวันออกเฉียงเหนือ" },
                    { 4, "ภาคใต้" },
                    { 5, "ภาคตะวันตก" },
                    { 6, "ภาคตะวันออก" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceId", "CountryId", "GeographyId", "ProvinceName" },
                values: new object[,]
                {
                    { 1, 1, 2, "Bangkok" },
                    { 2, 1, 1, "Chiang Mai" },
                    { 3, 2, 2, "Tokyo" },
                    { 4, 2, 2, "Osaka" },
                    { 5, 3, 2, "California" },
                    { 6, 3, 2, "New York" }
                });

            migrationBuilder.InsertData(
                table: "ThaiProvinces",
                columns: new[] { "ThaiProvinceId", "CountryId", "GeographyId", "ThaiProvinceName" },
                values: new object[,]
                {
                    { 1, 1, 2, "Bangkok" },
                    { 2, 1, 1, "Chiang Mai" },
                    { 3, 1, 1, "Chiang Rai" },
                    { 4, 1, 4, "Phuket" },
                    { 5, 1, 3, "Khon Kaen" },
                    { 6, 1, 3, "Nakhon Ratchasima" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "CountryModelCountryId", "ProvinceId", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, null, 1, "123 Sukhumvit", "10110" },
                    { 2, null, 2, "456 Nimman", "50000" },
                    { 3, null, 3, "789 Shibuya", "150-0002" },
                    { 4, null, 5, "101 Manhattan", "10001" }
                });

            migrationBuilder.InsertData(
                table: "Shippings",
                columns: new[] { "shipping_id", "CountryModelCountryId", "ProvinceId", "subDistrict" },
                values: new object[,]
                {
                    { 1, null, 1, "Wattana" },
                    { 2, null, 3, "Shibuya" },
                    { 3, null, 5, "Brooklyn" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryModelCountryId",
                table: "Addresses",
                column: "CountryModelCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProvinceId",
                table: "Addresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_GeographyId",
                table: "Provinces",
                column: "GeographyId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_CountryModelCountryId",
                table: "Shippings",
                column: "CountryModelCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_ProvinceId",
                table: "Shippings",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ThaiProvinces_CountryId",
                table: "ThaiProvinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ThaiProvinces_GeographyId",
                table: "ThaiProvinces",
                column: "GeographyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropTable(
                name: "ThaiProvinces");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "GeographyModel");
        }
    }
}
