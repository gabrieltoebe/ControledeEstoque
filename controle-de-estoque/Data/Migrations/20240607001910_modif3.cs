using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class modif3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioProduto_Inventario_InventarioIdInv",
                table: "InventarioProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_InventarioProduto_Produto_ProdutoCodProduto",
                table: "InventarioProduto");

            migrationBuilder.DropIndex(
                name: "IX_InventarioProduto_InventarioIdInv",
                table: "InventarioProduto");

            migrationBuilder.DropIndex(
                name: "IX_InventarioProduto_ProdutoCodProduto",
                table: "InventarioProduto");

            migrationBuilder.DropColumn(
                name: "InventarioIdInv",
                table: "InventarioProduto");

            migrationBuilder.DropColumn(
                name: "ProdutoCodProduto",
                table: "InventarioProduto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProduto_CodProduto",
                table: "InventarioProduto",
                column: "CodProduto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProduto_IdInv",
                table: "InventarioProduto",
                column: "IdInv");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioProduto_Inventario_IdInv",
                table: "InventarioProduto",
                column: "IdInv",
                principalTable: "Inventario",
                principalColumn: "IdInv",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioProduto_Produto_CodProduto",
                table: "InventarioProduto",
                column: "CodProduto",
                principalTable: "Produto",
                principalColumn: "CodProduto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioProduto_Inventario_IdInv",
                table: "InventarioProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_InventarioProduto_Produto_CodProduto",
                table: "InventarioProduto");

            migrationBuilder.DropIndex(
                name: "IX_InventarioProduto_CodProduto",
                table: "InventarioProduto");

            migrationBuilder.DropIndex(
                name: "IX_InventarioProduto_IdInv",
                table: "InventarioProduto");

            migrationBuilder.AddColumn<int>(
                name: "InventarioIdInv",
                table: "InventarioProduto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoCodProduto",
                table: "InventarioProduto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProduto_InventarioIdInv",
                table: "InventarioProduto",
                column: "InventarioIdInv");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProduto_ProdutoCodProduto",
                table: "InventarioProduto",
                column: "ProdutoCodProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioProduto_Inventario_InventarioIdInv",
                table: "InventarioProduto",
                column: "InventarioIdInv",
                principalTable: "Inventario",
                principalColumn: "IdInv",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioProduto_Produto_ProdutoCodProduto",
                table: "InventarioProduto",
                column: "ProdutoCodProduto",
                principalTable: "Produto",
                principalColumn: "CodProduto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
