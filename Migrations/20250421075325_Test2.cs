using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiNet8.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerModel_GeneralModel_GeneralId",
                table: "CustomerModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerModel",
                table: "CustomerModel");

            migrationBuilder.RenameTable(
                name: "CustomerModel",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerModel_GeneralId",
                table: "Customers",
                newName: "IX_Customers_GeneralId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_GeneralModel_GeneralId",
                table: "Customers",
                column: "GeneralId",
                principalTable: "GeneralModel",
                principalColumn: "general_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_GeneralModel_GeneralId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "CustomerModel");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_GeneralId",
                table: "CustomerModel",
                newName: "IX_CustomerModel_GeneralId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerModel",
                table: "CustomerModel",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerModel_GeneralModel_GeneralId",
                table: "CustomerModel",
                column: "GeneralId",
                principalTable: "GeneralModel",
                principalColumn: "general_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
