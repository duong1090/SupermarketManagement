using Microsoft.EntityFrameworkCore.Migrations;

namespace SupermarketManagement.Data.Migrations
{
    public partial class v : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_CustomerID",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Bills",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_CustomerID",
                table: "Bills",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_CustomerID",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_CustomerID",
                table: "Bills",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
