using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "ID", "Message", "PostDate", "Title" },
                values: new object[] { 1, "Alpha verzija aplikacije je online...", new DateTime(2021, 4, 19, 10, 41, 6, 293, DateTimeKind.Local).AddTicks(6997), "Alpha verzija online" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "ID", "Message", "PostDate", "Title" },
                values: new object[] { 2, "Beta većina značajki je funkcionalna", new DateTime(2021, 4, 24, 10, 41, 6, 296, DateTimeKind.Local).AddTicks(3945), "Beta verzija online" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "ID", "Message", "PostDate", "Title" },
                values: new object[] { 3, "Prva release verzija online...", new DateTime(2021, 4, 29, 10, 41, 6, 296, DateTimeKind.Local).AddTicks(4054), "v1.01 online" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
