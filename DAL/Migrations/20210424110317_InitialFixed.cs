using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AdCategories_CategoryId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Counties_CountyId",
                table: "Ads");

            migrationBuilder.AlterColumn<int>(
                name: "CountyId",
                table: "Ads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Ads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AdCategories_CategoryId",
                table: "Ads",
                column: "CategoryId",
                principalTable: "AdCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Counties_CountyId",
                table: "Ads",
                column: "CountyId",
                principalTable: "Counties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AdCategories_CategoryId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Counties_CountyId",
                table: "Ads");

            migrationBuilder.AlterColumn<int>(
                name: "CountyId",
                table: "Ads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Ads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AdCategories_CategoryId",
                table: "Ads",
                column: "CategoryId",
                principalTable: "AdCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Counties_CountyId",
                table: "Ads",
                column: "CountyId",
                principalTable: "Counties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
