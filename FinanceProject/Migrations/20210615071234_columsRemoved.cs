using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceProject.Migrations
{
    public partial class columsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Removed",
                table: "ItemsAccountsReceivables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Removed",
                table: "ItemsAccountsPayables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Removed",
                table: "accountsReceivables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Removed",
                table: "AccountsPayables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Removed",
                table: "ItemsAccountsReceivables");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "ItemsAccountsPayables");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "accountsReceivables");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "AccountsPayables");
        }
    }
}
