using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMS.Core.Migrations
{
    public partial class UpdatePaymentComplitedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Completed",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApartmentId",
                table: "Payments",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Apartments_ApartmentId",
                table: "Payments",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Apartments_ApartmentId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ApartmentId",
                table: "Payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Completed",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
