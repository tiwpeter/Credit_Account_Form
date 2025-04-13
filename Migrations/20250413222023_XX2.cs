using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeographyId",
                table: "ThaiProvinces",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ThaiProvinces_GeographyId",
                table: "ThaiProvinces",
                column: "GeographyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThaiProvinces_GeographyModel_GeographyId",
                table: "ThaiProvinces",
                column: "GeographyId",
                principalTable: "GeographyModel",
                principalColumn: "GeographyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThaiProvinces_GeographyModel_GeographyId",
                table: "ThaiProvinces");

            migrationBuilder.DropTable(
                name: "GeographyModel");

            migrationBuilder.DropIndex(
                name: "IX_ThaiProvinces_GeographyId",
                table: "ThaiProvinces");

            migrationBuilder.DropColumn(
                name: "GeographyId",
                table: "ThaiProvinces");
        }
    }
}
