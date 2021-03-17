using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Steps.Migrations
{
    public partial class AddingFitspo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPost",
                table: "Fitspos");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Fitspos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Fitspos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPost",
                table: "Fitspos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
