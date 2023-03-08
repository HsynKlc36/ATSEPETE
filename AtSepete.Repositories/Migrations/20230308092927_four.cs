using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtSepete.Repositories.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 0);
        }
    }
}
