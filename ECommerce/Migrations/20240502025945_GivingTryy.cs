using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class GivingTryy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInPromotion_Products_ProductId1",
                table: "ProductInPromotion");

            migrationBuilder.DropIndex(
                name: "IX_ProductInPromotion_ProductId1",
                table: "ProductInPromotion");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductInPromotion");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductInPromotion",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInPromotion_ProductId",
                table: "ProductInPromotion",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInPromotion_Products_ProductId",
                table: "ProductInPromotion",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInPromotion_Products_ProductId",
                table: "ProductInPromotion");

            migrationBuilder.DropIndex(
                name: "IX_ProductInPromotion_ProductId",
                table: "ProductInPromotion");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductInPromotion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductInPromotion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInPromotion_ProductId1",
                table: "ProductInPromotion",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInPromotion_Products_ProductId1",
                table: "ProductInPromotion",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
