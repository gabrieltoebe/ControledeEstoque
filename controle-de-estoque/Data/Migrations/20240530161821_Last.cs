using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class Last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produto_ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Estoque_EstoqueIdEstoque",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Estoque_IdEstoque",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_IdEstoque",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_EstoqueIdEstoque",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "IdEstoque",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "EstoqueIdEstoque",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "TipoMov",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "ProdutoCodProduto",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "QuantidadeDeItensNoEstoque",
                table: "Estoque");

            migrationBuilder.AddColumn<string>(
                name: "CpfId",
                table: "InventarioProduto",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoMov",
                table: "InventarioProduto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProduto_CpfId",
                table: "InventarioProduto",
                column: "CpfId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_IdEstoque",
                table: "Inventario",
                column: "IdEstoque");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Estoque_IdEstoque",
                table: "Inventario",
                column: "IdEstoque",
                principalTable: "Estoque",
                principalColumn: "IdEstoque",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioProduto_AspNetUsers_CpfId",
                table: "InventarioProduto",
                column: "CpfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Estoque_IdEstoque",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_InventarioProduto_AspNetUsers_CpfId",
                table: "InventarioProduto");

            migrationBuilder.DropIndex(
                name: "IX_InventarioProduto_CpfId",
                table: "InventarioProduto");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_IdEstoque",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "CpfId",
                table: "InventarioProduto");

            migrationBuilder.DropColumn(
                name: "TipoMov",
                table: "InventarioProduto");

            migrationBuilder.AddColumn<int>(
                name: "IdEstoque",
                table: "Produto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstoqueIdEstoque",
                table: "Inventario",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoMov",
                table: "Inventario",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoCodProduto",
                table: "Estoque",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeDeItensNoEstoque",
                table: "Estoque",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdEstoque",
                table: "Produto",
                column: "IdEstoque");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_EstoqueIdEstoque",
                table: "Inventario",
                column: "EstoqueIdEstoque");

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
                name: "FK_Inventario_Estoque_EstoqueIdEstoque",
                table: "Inventario",
                column: "EstoqueIdEstoque",
                principalTable: "Estoque",
                principalColumn: "IdEstoque");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Estoque_IdEstoque",
                table: "Produto",
                column: "IdEstoque",
                principalTable: "Estoque",
                principalColumn: "IdEstoque",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
