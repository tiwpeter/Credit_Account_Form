using System;
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
            migrationBuilder.CreateTable(
                name: "accountGroup",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accGroupDes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accountGroup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessType",
                columns: table => new
                {
                    busiTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    busiTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    busiTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    busiTypeDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessType", x => x.busiTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyAddr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IndustryType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InduTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InduTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InduTypeDes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrg",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saleOrgCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saleOrgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saleOrgDes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrg", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shopType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InduTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TestModel",
                columns: table => new
                {
                    Test = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModel", x => x.Test);
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
                name: "RegisterForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    Test1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisterForms_TestModel_Test1",
                        column: x => x.Test1,
                        principalTable: "TestModel",
                        principalColumn: "Test",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    shipping_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.shipping_id);
                    table.ForeignKey(
                        name: "FK_Shippings_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    ThaiProvinceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId");
                    table.ForeignKey(
                        name: "FK_Addresses_ThaiProvinces_ThaiProvinceId",
                        column: x => x.ThaiProvinceId,
                        principalTable: "ThaiProvinces",
                        principalColumn: "ThaiProvinceId");
                });

            migrationBuilder.CreateTable(
                name: "Regisforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regisforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regisforms_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { 1, "ประเทศไทย" },
                    { 2, "สหรัฐอเมริกา" }
                });

            migrationBuilder.InsertData(
                table: "GeographyModel",
                columns: new[] { "GeographyId", "GeographyName" },
                values: new object[,]
                {
                    { 1, "ภาคเหนือ" },
                    { 2, "ภาคกลาง" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceId", "CountryId", "GeographyId", "ProvinceName" },
                values: new object[,]
                {
                    { 1, 2, 2, "California" },
                    { 2, 2, 2, "New York" }
                });

            migrationBuilder.InsertData(
                table: "ThaiProvinces",
                columns: new[] { "ThaiProvinceId", "CountryId", "GeographyId", "ThaiProvinceName" },
                values: new object[,]
                {
                    { 1, 1, 1, "เชียงใหม่" },
                    { 2, 1, 2, "กรุงเทพมหานคร" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "CountryId", "ProvinceId", "Street", "ThaiProvinceId" },
                values: new object[,]
                {
                    { 1, 1, null, "123 ถนนราชดำเนิน", 2 },
                    { 2, 2, 1, "456 Sunset Blvd", null }
                });

            migrationBuilder.InsertData(
                table: "Regisforms",
                columns: new[] { "Id", "AddressId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProvinceId",
                table: "Addresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ThaiProvinceId",
                table: "Addresses",
                column: "ThaiProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_GeographyId",
                table: "Provinces",
                column: "GeographyId");

            migrationBuilder.CreateIndex(
                name: "IX_Regisforms_AddressId",
                table: "Regisforms",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterForms_Test1",
                table: "RegisterForms",
                column: "Test1");

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
                name: "accountGroup");

            migrationBuilder.DropTable(
                name: "BusinessType");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "IndustryType");

            migrationBuilder.DropTable(
                name: "Regisforms");

            migrationBuilder.DropTable(
                name: "RegisterForms");

            migrationBuilder.DropTable(
                name: "SaleOrg");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropTable(
                name: "shopType");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "TestModel");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "ThaiProvinces");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "GeographyModel");
        }
    }
}
