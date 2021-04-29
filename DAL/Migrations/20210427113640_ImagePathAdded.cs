using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ImagePathAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Ads");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Ads",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Ads");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Ads",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
