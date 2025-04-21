using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class Test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmphureModel_Provinces_ProvinceModelProvinceId",
                table: "AmphureModel");

            migrationBuilder.DropIndex(
                name: "IX_AmphureModel_ProvinceModelProvinceId",
                table: "AmphureModel");

            migrationBuilder.DropColumn(
                name: "ProvinceModelProvinceId",
                table: "AmphureModel");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "AmphureModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GeneralModel",
                columns: table => new
                {
                    general_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    generalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
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
                    TambonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmphureId = table.Column<int>(type: "int", nullable: false)
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
                name: "CustomerModel",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerModel", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_CustomerModel_GeneralModel_GeneralId",
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
                table: "CustomerModel",
                columns: new[] { "CustomerId", "CustomerName", "GeneralId" },
                values: new object[,]
                {
                    { 1, "ลูกค้า A", 1 },
                    { 2, "ลูกค้า B", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmphureModel_ProvinceId",
                table: "AmphureModel",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerModel_GeneralId",
                table: "CustomerModel",
                column: "GeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralModel_AddressId",
                table: "GeneralModel",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TambonModel_AmphureId",
                table: "TambonModel",
                column: "AmphureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AmphureModel_Provinces_ProvinceId",
                table: "AmphureModel",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmphureModel_Provinces_ProvinceId",
                table: "AmphureModel");

            migrationBuilder.DropTable(
                name: "CustomerModel");

            migrationBuilder.DropTable(
                name: "TambonModel");

            migrationBuilder.DropTable(
                name: "GeneralModel");

            migrationBuilder.DropIndex(
                name: "IX_AmphureModel_ProvinceId",
                table: "AmphureModel");

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AmphureModel",
                keyColumn: "AmphureId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AmphureModel",
                keyColumn: "AmphureId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CountryModel",
                keyColumn: "CountryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Geography",
                keyColumn: "GeographyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Geography",
                keyColumn: "GeographyId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "AmphureModel");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceModelProvinceId",
                table: "AmphureModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmphureModel_ProvinceModelProvinceId",
                table: "AmphureModel",
                column: "ProvinceModelProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AmphureModel_Provinces_ProvinceModelProvinceId",
                table: "AmphureModel",
                column: "ProvinceModelProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId");
        }
    }
}
