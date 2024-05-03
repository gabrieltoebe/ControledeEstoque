using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class AttProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoCodProduto",
                table: "Estoque",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdEstoque",
                table: "Produto",
                column: "IdEstoque");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_ProdutoCodProduto",
                table: "Estoque",
                column: "ProdutoCodProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Produto_ProdutoCodProduto",
                table: "Estoque",
                column: "ProdutoCodProduto",
                principalTable: "Produto",
                principalColumn: "CodProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Estoque_IdEstoque",
                table: "Produto",
                column: "IdEstoque",
                principalTable: "Estoque",
                principalColumn: "IdEstoque",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produto_ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Estoque_IdEstoque",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_IdEstoque",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "ProdutoCodProduto",
                table: "Estoque");
        }
    }
}
