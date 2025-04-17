using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX3 : Migration
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
                    RegistrationDate = table.Column<DateTime>(type: "date", nullable: true),
                    RegisteredCapital = table.Column<decimal>(type: "decimal(15,2)", nullable: true)
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

            migrationBuilder.InsertData(
                table: "BusinessType",
                columns: new[] { "busiTypeID", "RegisteredCapital", "RegistrationDate", "busiTypeCode", "busiTypeDes", "busiTypeName" },
                values: new object[,]
                {
                    { 1, 1000000.00m, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RET", "ธุรกิจค้าปลีกทั่วไป", "ค้าปลีก" },
                    { 2, 5000000.00m, new DateTime(2011, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "WHL", "ธุรกิจค้าส่งสินค้า", "ค้าส่ง" },
                    { 3, 15000000.00m, new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "MFG", "โรงงานผลิตสินค้าทั่วไป", "การผลิต" },
                    { 4, 2000000.00m, new DateTime(2018, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "SRV", "บริษัทให้บริการทั่วไป", "บริการ" },
                    { 5, null, new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GOV", "องค์กรภาครัฐ", "หน่วยงานราชการ" }
                });
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
                name: "SaleOrg");

            migrationBuilder.DropTable(
                name: "shopType");
        }
    }
}
