using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class appuserupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Favorites3",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CustomerCreatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CustomerFirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerLastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CustomerModifiedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "CustomerStatus",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites3_UserId",
                table: "Favorites3",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites3_AspNetUsers_UserId",
                table: "Favorites3",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites3_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites3_UserId",
                table: "Favorites3");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favorites3");

            migrationBuilder.DropColumn(
                name: "CustomerCreatedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerFirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerLastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerModifiedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerStatus",
                table: "AspNetUsers");
        }
    }
}
