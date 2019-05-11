using Microsoft.EntityFrameworkCore.Migrations;

namespace AMS.Core.Migrations
{
    public partial class UpdatePaymentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
