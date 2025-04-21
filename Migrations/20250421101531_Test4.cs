using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class Test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "TambonModel");

            migrationBuilder.DropTable(
                name: "GeneralModel");

            migrationBuilder.DropTable(
                name: "AmphureModel");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "CountryModel");

            migrationBuilder.DropTable(
                name: "Geography");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryModel",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryModel", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Geography",
                columns: table => new
                {
                    GeographyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeographyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geography", x => x.GeographyId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    GeographyId = table.Column<int>(type: "int", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_Provinces_CountryModel_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryModel",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provinces_Geography_GeographyId",
                        column: x => x.GeographyId,
                        principalTable: "Geography",
                        principalColumn: "GeographyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CountryModelCountryId = table.Column<int>(type: "int", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_CountryModel_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryModel",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_CountryModel_CountryModelCountryId",
                        column: x => x.CountryModelCountryId,
                        principalTable: "CountryModel",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_Addresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AmphureModel",
                columns: table => new
                {
                    AmphureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    AmphureName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmphureModel", x => x.AmphureId);
                    table.ForeignKey(
                        name: "FK_AmphureModel_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralModel",
                columns: table => new
                {
                    general_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    generalName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralModel", x => x.general_id);
                    table.ForeignKey(
                        name: "FK_GeneralModel_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TambonModel",
                columns: table => new
                {
                    TambonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmphureId = table.Column<int>(type: "int", nullable: false),
                    TambonName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TambonModel", x => x.TambonId);
                    table.ForeignKey(
                        name: "FK_TambonModel_AmphureModel_AmphureId",
                        column: x => x.AmphureId,
                        principalTable: "AmphureModel",
                        principalColumn: "AmphureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_GeneralModel_GeneralId",
                        column: x => x.GeneralId,
                        principalTable: "GeneralModel",
                        principalColumn: "general_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CountryModel",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[] { 1, "Thailand" });

            migrationBuilder.InsertData(
                table: "Geography",
                columns: new[] { "GeographyId", "GeographyName" },
                values: new object[,]
                {
                    { 1, "ภาคกลาง" },
                    { 2, "ภาคเหนือ" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceId", "CountryId", "GeographyId", "ProvinceName" },
                values: new object[,]
                {
                    { 1, 1, 1, "กรุงเทพมหานคร" },
                    { 2, 1, 2, "เชียงใหม่" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "CountryId", "CountryModelCountryId", "ProvinceId", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, 1, null, 1, "123 ถนนสีลม", "10500" },
                    { 2, 1, null, 2, "456 ถ.สุเทพ", "50200" }
                });

            migrationBuilder.InsertData(
                table: "AmphureModel",
                columns: new[] { "AmphureId", "AmphureName", "ProvinceId" },
                values: new object[,]
                {
                    { 1, "เขตบางรัก", 1 },
                    { 2, "อำเภอเมืองเชียงใหม่", 2 }
                });

            migrationBuilder.InsertData(
                table: "GeneralModel",
                columns: new[] { "general_id", "AddressId", "generalName" },
                values: new object[,]
                {
                    { 1, 1, "นายสมชาย" },
                    { 2, 2, "นางสาวดารา" }
                });

            migrationBuilder.InsertData(
                table: "TambonModel",
                columns: new[] { "TambonId", "AmphureId", "TambonName" },
                values: new object[,]
                {
                    { 1, 1, "สีลม" },
                    { 2, 2, "สุเทพ" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerName", "GeneralId" },
                values: new object[,]
                {
                    { 1, "ลูกค้า A", 1 },
                    { 2, "ลูกค้า B", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryModelCountryId",
                table: "Addresses",
                column: "CountryModelCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProvinceId",
                table: "Addresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AmphureModel_ProvinceId",
                table: "AmphureModel",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GeneralId",
                table: "Customers",
                column: "GeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralModel_AddressId",
                table: "GeneralModel",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_GeographyId",
                table: "Provinces",
                column: "GeographyId");

            migrationBuilder.CreateIndex(
                name: "IX_TambonModel_AmphureId",
                table: "TambonModel",
                column: "AmphureId");
        }
    }
}
