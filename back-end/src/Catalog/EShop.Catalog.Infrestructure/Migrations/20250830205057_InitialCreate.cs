using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Catalog.Infrestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(28,2)", precision: 28, scale: 2, nullable: false),
                    PictureFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailableStock = table.Column<int>(type: "int", nullable: false),
                    RestockThreshold = table.Column<int>(type: "int", nullable: false),
                    MaxStockThreshold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItens_CatalogBrands_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalTable: "CatalogBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CatalogBrands",
                columns: new[] { "Id", "Brand" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Nike" },
                    { new Guid("e42a20db-bd93-4897-829f-a2b436ceff7c"), "Louis Vuitton" },
                    { new Guid("edef3c70-38fb-44b1-8028-e620c42b6c6f"), "Adidas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItens_CatalogBrandId",
                table: "CatalogItens",
                column: "CatalogBrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItens");

            migrationBuilder.DropTable(
                name: "CatalogBrands");
        }
    }
}
