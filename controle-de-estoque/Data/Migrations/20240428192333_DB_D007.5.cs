using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class DB_D0075 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdEstoque",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<string>(
                name: "CpfId",
                table: "ProdutoFornecedorReceb",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoFornecedor",
                table: "Fornecedor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstadoFornecedor",
                table: "Fornecedor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelefoneFornecedor",
                table: "Fornecedor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CpfId",
                table: "EstoqueProduto",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfId",
                table: "Estoque",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedorReceb_CpfId",
                table: "ProdutoFornecedorReceb",
                column: "CpfId");

            migrationBuilder.CreateIndex(
                name: "IX_EstoqueProduto_CpfId",
                table: "EstoqueProduto",
                column: "CpfId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_CpfId",
                table: "Estoque",
                column: "CpfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_AspNetUsers_CpfId",
                table: "Estoque",
                column: "CpfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstoqueProduto_AspNetUsers_CpfId",
                table: "EstoqueProduto",
                column: "CpfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoFornecedorReceb_AspNetUsers_CpfId",
                table: "ProdutoFornecedorReceb",
                column: "CpfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_AspNetUsers_CpfId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_EstoqueProduto_AspNetUsers_CpfId",
                table: "EstoqueProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoFornecedorReceb_AspNetUsers_CpfId",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoFornecedorReceb_CpfId",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.DropIndex(
                name: "IX_EstoqueProduto_CpfId",
                table: "EstoqueProduto");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_CpfId",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "CpfId",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.DropColumn(
                name: "EnderecoFornecedor",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EstadoFornecedor",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "TelefoneFornecedor",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "CpfId",
                table: "EstoqueProduto");

            migrationBuilder.DropColumn(
                name: "CpfId",
                table: "Estoque");

            migrationBuilder.AlterColumn<int>(
                name: "IdEstoque",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);
        }
    }
}
