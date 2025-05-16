using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class NewGenerralModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessTypes",
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
                    table.PrimaryKey("PK_BusinessTypes", x => x.busiTypeID);
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
                name: "CreditInfo",
                columns: table => new
                {
                    CreditInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstimatedPurchase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeRequired = table.Column<int>(type: "int", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditInfo", x => x.CreditInfoId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSign",
                columns: table => new
                {
                    CustSignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustSignFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    custsignTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    custsignEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    custsignLine = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSign", x => x.CustSignId);
                });

            migrationBuilder.CreateTable(
                name: "DocCreditModel",
                columns: table => new
                {
                    DocCreditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCertificate = table.Column<bool>(type: "bit", nullable: false),
                    CopyOfPP_20 = table.Column<bool>(type: "bit", nullable: false),
                    CopyOfCoRegis = table.Column<bool>(type: "bit", nullable: false),
                    CopyOfIDCard = table.Column<bool>(type: "bit", nullable: false),
                    CompanyLocationMap = table.Column<bool>(type: "bit", nullable: false),
                    OtherSpecify = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocCreditModel", x => x.DocCreditId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addrLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addrLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    shipping_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    freight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.shipping_id);
                    table.ForeignKey(
                        name: "FK_Shippings_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Generals",
                columns: table => new
                {
                    general_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralTax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralBranch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generals", x => x.general_id);
                    table.ForeignKey(
                        name: "FK_Generals_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralId = table.Column<int>(type: "int", nullable: false),
                    shipping_id = table.Column<int>(type: "int", nullable: false),
                    IndustryType_InduTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryType_InduTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryType_InduTypeDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_companyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_companyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_companyAddr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrg_saleOrgCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrg_saleOrgName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleOrg_saleOrgDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountCode_AccountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountCode_AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountCode_AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountCode_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    busiTypeID = table.Column<int>(type: "int", nullable: false),
                    CreditInfoId = table.Column<int>(type: "int", nullable: true),
                    DocCreditId = table.Column<int>(type: "int", nullable: true),
                    CustSignId = table.Column<int>(type: "int", nullable: true),
                    accountGroup_accGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accountGroup_accGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accountGroup_accGroupDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortKey_sortkeyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortKey_sortkeyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortKey_sortkeyDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashGroup_CashGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashGroup_CashGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashGroup_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod_PaymentMethodCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod_PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermOfPayment_TermCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermOfPayment_TermName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermOfPayment_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleDistrict_DistrictCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleDistrict_DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleDistrict_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleGroup_GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleGroup_GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleGroup_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustGroupType_GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustGroupType_GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustGroupType_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency_CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency_CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency_Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchRateType_RateTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchRateType_RateTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchRateType_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustPricProc_PricProcCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustPricProc_PricProcName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustPricProc_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceList_priceListCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceList_priceListName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceList_priceListDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Incoterm_incotermCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Incoterm_incotermName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Incoterm_incotermDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleManager_SaleGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleManager_SaleGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleManager_SaleGroupDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustGroupCountry_CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustGroupCountry_CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustGroupCountry_CountryDes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_BusinessTypes_busiTypeID",
                        column: x => x.busiTypeID,
                        principalTable: "BusinessTypes",
                        principalColumn: "busiTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_CreditInfo_CreditInfoId",
                        column: x => x.CreditInfoId,
                        principalTable: "CreditInfo",
                        principalColumn: "CreditInfoId");
                    table.ForeignKey(
                        name: "FK_Customers_CustomerSign_CustSignId",
                        column: x => x.CustSignId,
                        principalTable: "CustomerSign",
                        principalColumn: "CustSignId");
                    table.ForeignKey(
                        name: "FK_Customers_DocCreditModel_DocCreditId",
                        column: x => x.DocCreditId,
                        principalTable: "DocCreditModel",
                        principalColumn: "DocCreditId");
                    table.ForeignKey(
                        name: "FK_Customers_Generals_GeneralId",
                        column: x => x.GeneralId,
                        principalTable: "Generals",
                        principalColumn: "general_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Shippings_shipping_id",
                        column: x => x.shipping_id,
                        principalTable: "Shippings",
                        principalColumn: "shipping_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopType",
                columns: table => new
                {
                    CustomerModelCustomerId = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    shopCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shopName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shopDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopType", x => x.CustomerModelCustomerId);
                    table.ForeignKey(
                        name: "FK_ShopType_Customers_CustomerModelCustomerId",
                        column: x => x.CustomerModelCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { 1, "Thailand" },
                    { 2, "Japan" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceId", "CountryId", "ProvinceName" },
                values: new object[,]
                {
                    { 1, 1, "Bangkok" },
                    { 2, 2, "Tokyo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProvinceId",
                table: "Addresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_busiTypeID",
                table: "Customers",
                column: "busiTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreditInfoId",
                table: "Customers",
                column: "CreditInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustSignId",
                table: "Customers",
                column: "CustSignId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DocCreditId",
                table: "Customers",
                column: "DocCreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GeneralId",
                table: "Customers",
                column: "GeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_shipping_id",
                table: "Customers",
                column: "shipping_id");

            migrationBuilder.CreateIndex(
                name: "IX_Generals_AddressId",
                table: "Generals",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_CountryId",
                table: "Shippings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_ProvinceId",
                table: "Shippings",
                column: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopType");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "BusinessTypes");

            migrationBuilder.DropTable(
                name: "CreditInfo");

            migrationBuilder.DropTable(
                name: "CustomerSign");

            migrationBuilder.DropTable(
                name: "DocCreditModel");

            migrationBuilder.DropTable(
                name: "Generals");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
