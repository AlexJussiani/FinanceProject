using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceProject.Migrations
{
    public partial class COntroller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsPayables_Customers_SupplierId",
                table: "AccountsPayables");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "AccountsPayables",
                newName: "SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_AccountsPayables_SupplierId",
                table: "AccountsPayables",
                newName: "IX_AccountsPayables_SupplierID");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierID",
                table: "AccountsPayables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsPayables_Customers_SupplierID",
                table: "AccountsPayables",
                column: "SupplierID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsPayables_Customers_SupplierID",
                table: "AccountsPayables");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                table: "AccountsPayables",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountsPayables_SupplierID",
                table: "AccountsPayables",
                newName: "IX_AccountsPayables_SupplierId");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "AccountsPayables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsPayables_Customers_SupplierId",
                table: "AccountsPayables",
                column: "SupplierId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
