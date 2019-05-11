using Microsoft.EntityFrameworkCore.Migrations;

namespace AMS.Core.Migrations
{
    public partial class RenamedRequestStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatusId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Requests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatusId",
                table: "Requests",
                nullable: false,
                defaultValue: 0);
        }
    }
}
