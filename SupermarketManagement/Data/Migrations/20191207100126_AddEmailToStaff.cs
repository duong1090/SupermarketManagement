using Microsoft.EntityFrameworkCore.Migrations;

namespace SupermarketManagement.Data.Migrations
{
    public partial class AddEmailToStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrie",
                table: "Bill_Details",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Staffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Staffs");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Bill_Details",
                newName: "TotalPrie");
        }
    }
}
