using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceProject.Migrations
{
    public partial class migracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Removed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    removed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountsPayables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsPayables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsPayables_Customers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "accountsReceivables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accountsReceivables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accountsReceivables_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsAccountsPayables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountsPayableId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitareValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsAccountsPayables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsAccountsPayables_AccountsPayables_accountsPayableId",
                        column: x => x.accountsPayableId,
                        principalTable: "AccountsPayables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsAccountsPayables_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsAccountsReceivables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountsPayableId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitareValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValueDiscont = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsAccountsReceivables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsAccountsReceivables_AccountsPayables_accountsPayableId",
                        column: x => x.accountsPayableId,
                        principalTable: "AccountsPayables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsAccountsReceivables_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountsMovimentations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    accountsPayableId = table.Column<int>(type: "int", nullable: true),
                    accountsReceivableId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsMovimentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsMovimentations_AccountsPayables_accountsPayableId",
                        column: x => x.accountsPayableId,
                        principalTable: "AccountsPayables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsMovimentations_accountsReceivables_accountsReceivableId",
                        column: x => x.accountsReceivableId,
                        principalTable: "accountsReceivables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsMovimentations_accountsPayableId",
                table: "AccountsMovimentations",
                column: "accountsPayableId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsMovimentations_accountsReceivableId",
                table: "AccountsMovimentations",
                column: "accountsReceivableId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayables_SupplierId",
                table: "AccountsPayables",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_accountsReceivables_CustomerId",
                table: "accountsReceivables",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsAccountsPayables_accountsPayableId",
                table: "ItemsAccountsPayables",
                column: "accountsPayableId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsAccountsPayables_ProductId",
                table: "ItemsAccountsPayables",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsAccountsReceivables_accountsPayableId",
                table: "ItemsAccountsReceivables",
                column: "accountsPayableId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsAccountsReceivables_ProductId",
                table: "ItemsAccountsReceivables",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsMovimentations");

            migrationBuilder.DropTable(
                name: "ItemsAccountsPayables");

            migrationBuilder.DropTable(
                name: "ItemsAccountsReceivables");

            migrationBuilder.DropTable(
                name: "accountsReceivables");

            migrationBuilder.DropTable(
                name: "AccountsPayables");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
