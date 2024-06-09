using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class modif4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoFornecedorReceb",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.AlterColumn<int>(
                name: "CodProduto",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "IdFornecedor",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProdFornRec",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoFornecedorReceb",
                table: "ProdutoFornecedorReceb",
                column: "IdProdFornRec");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedorReceb_IdFornecedor",
                table: "ProdutoFornecedorReceb",
                column: "IdFornecedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoFornecedorReceb",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoFornecedorReceb_IdFornecedor",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.DropColumn(
                name: "IdProdFornRec",
                table: "ProdutoFornecedorReceb");

            migrationBuilder.AlterColumn<int>(
                name: "IdFornecedor",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "CodProduto",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoFornecedorReceb",
                table: "ProdutoFornecedorReceb",
                columns: new[] { "IdFornecedor", "CodProduto" });
        }
    }
}
