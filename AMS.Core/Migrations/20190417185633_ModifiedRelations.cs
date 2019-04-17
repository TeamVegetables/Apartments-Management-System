using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMS.Core.Migrations
{
    public partial class ModifiedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_ApartmentStatuses_ApartmentStatusId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Apartments_ApartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Apartments_ApartmentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentStatuses_PaymentStatusId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_UserId1",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentInfos_Apartments_ApartmentId",
                table: "RentInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_RentInfos_AspNetUsers_UserId1",
                table: "RentInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestStatuses_RequestStatusId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestStatusId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_RentInfos_ApartmentId",
                table: "RentInfos");

            migrationBuilder.DropIndex(
                name: "IX_RentInfos_UserId1",
                table: "RentInfos");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ApartmentId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentStatusId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_ApartmentStatusId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "RentInfos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "RentInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStatusId",
                table: "Requests",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserId",
                table: "Requests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentInfos_ApartmentId",
                table: "RentInfos",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RentInfos_UserId1",
                table: "RentInfos",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApartmentId",
                table: "Payments",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStatusId",
                table: "Payments",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId1",
                table: "Payments",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApartmentId",
                table: "AspNetUsers",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_ApartmentStatusId",
                table: "Apartments",
                column: "ApartmentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_ApartmentStatuses_ApartmentStatusId",
                table: "Apartments",
                column: "ApartmentStatusId",
                principalTable: "ApartmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Apartments_ApartmentId",
                table: "AspNetUsers",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Apartments_ApartmentId",
                table: "Payments",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentStatuses_PaymentStatusId",
                table: "Payments",
                column: "PaymentStatusId",
                principalTable: "PaymentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_UserId1",
                table: "Payments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentInfos_Apartments_ApartmentId",
                table: "RentInfos",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentInfos_AspNetUsers_UserId1",
                table: "RentInfos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestStatuses_RequestStatusId",
                table: "Requests",
                column: "RequestStatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
