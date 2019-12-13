using Microsoft.EntityFrameworkCore.Migrations;

namespace SupermarketManagement.Data.Migrations
{
    public partial class NewDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bill_Details_Bill_DetailID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Receipt_Details_Receipt_DetailID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Bill_DetailID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Receipt_DetailID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Bill_DetailID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Receipt_DetailID",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_Details_ProductID",
                table: "Receipt_Details",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_Details_ProductID",
                table: "Bill_Details",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Details_Products_ProductID",
                table: "Bill_Details",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Details_Products_ProductID",
                table: "Receipt_Details",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Details_Products_ProductID",
                table: "Bill_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Details_Products_ProductID",
                table: "Receipt_Details");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_Details_ProductID",
                table: "Receipt_Details");

            migrationBuilder.DropIndex(
                name: "IX_Bill_Details_ProductID",
                table: "Bill_Details");

            migrationBuilder.AddColumn<int>(
                name: "Bill_DetailID",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Receipt_DetailID",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Bill_DetailID",
                table: "Products",
                column: "Bill_DetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Receipt_DetailID",
                table: "Products",
                column: "Receipt_DetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bill_Details_Bill_DetailID",
                table: "Products",
                column: "Bill_DetailID",
                principalTable: "Bill_Details",
                principalColumn: "Bill_DetailID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Receipt_Details_Receipt_DetailID",
                table: "Products",
                column: "Receipt_DetailID",
                principalTable: "Receipt_Details",
                principalColumn: "Receipt_DetailID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
