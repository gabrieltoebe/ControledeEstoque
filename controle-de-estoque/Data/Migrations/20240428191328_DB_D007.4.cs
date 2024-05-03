using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class DB_D0074 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEstoque",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedorReceb_IdEstoque",
                table: "ProdutoFornecedorReceb",
                column: "IdEstoque");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoFornecedorReceb_Estoque_IdEstoque",
                table: "ProdutoFornecedorReceb",
                column: "IdEstoque",
                principalTable: "Estoque",
                principalColumn: "IdEstoque",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoFornecedorReceb_Estoque_IdEstoque",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoFornecedorReceb_IdEstoque",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.DropColumn(
                name: "IdEstoque",
                table: "ProdutoFornecedorReceb");
        }
    }
}
