using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class XX12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regisforms_Addresses_AddressId",
                table: "Regisforms");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Regisforms",
                newName: "GeneralId");

            migrationBuilder.RenameIndex(
                name: "IX_Regisforms_AddressId",
                table: "Regisforms",
                newName: "IX_Regisforms_GeneralId");

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

            migrationBuilder.InsertData(
                table: "GeneralModel",
                columns: new[] { "general_id", "AddressId", "generalName" },
                values: new object[,]
                {
                    { 1, 1, "John Doe" },
                    { 2, 2, "Jane Smith" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralModel_AddressId",
                table: "GeneralModel",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regisforms_GeneralModel_GeneralId",
                table: "Regisforms",
                column: "GeneralId",
                principalTable: "GeneralModel",
                principalColumn: "general_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regisforms_GeneralModel_GeneralId",
                table: "Regisforms");

            migrationBuilder.DropTable(
                name: "GeneralModel");

            migrationBuilder.RenameColumn(
                name: "GeneralId",
                table: "Regisforms",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Regisforms_GeneralId",
                table: "Regisforms",
                newName: "IX_Regisforms_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regisforms_Addresses_AddressId",
                table: "Regisforms",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId");
        }
    }
}
