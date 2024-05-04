using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddedPromotionX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    IdPromotion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontantReduction = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.IdPromotion);
                });

            migrationBuilder.CreateTable(
                name: "ProductInPromotion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReductionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromotionIdPromotion = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInPromotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInPromotion_Promotions_PromotionIdPromotion",
                        column: x => x.PromotionIdPromotion,
                        principalTable: "Promotions",
                        principalColumn: "IdPromotion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInPromotion_PromotionIdPromotion",
                table: "ProductInPromotion",
                column: "PromotionIdPromotion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInPromotion");

            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}
