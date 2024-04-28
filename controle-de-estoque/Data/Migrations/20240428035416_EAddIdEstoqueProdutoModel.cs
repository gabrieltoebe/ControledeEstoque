using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class EAddIdEstoqueProdutoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produto_ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.AddColumn<int>(
                name: "IdEstoque",
                table: "Produto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEstoque",
                table: "Produto");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoCodProduto",
                table: "Estoque",
                type: "INTEGER",
                nullable: true);

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
        }
    }
}
