using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMS.Core.Migrations
{
    public partial class UpdatePaymentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatusId",
                table: "Payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLine",
                table: "Payments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatusId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
