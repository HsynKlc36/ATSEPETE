using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtSepete.Repositories.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Products_ProductId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProductId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
        }
    }
}
