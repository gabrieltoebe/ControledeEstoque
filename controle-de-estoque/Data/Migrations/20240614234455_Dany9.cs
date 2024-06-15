using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class Dany9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoCliente",
                table: "ProdutoCliente");

            migrationBuilder.AlterColumn<int>(
                name: "IdProdClient",
                table: "ProdutoCliente",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoCliente",
                table: "ProdutoCliente",
                column: "IdProdClient");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCliente_IdCliente",
                table: "ProdutoCliente",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoCliente",
                table: "ProdutoCliente");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoCliente_IdCliente",
                table: "ProdutoCliente");

            migrationBuilder.AlterColumn<int>(
                name: "IdProdClient",
                table: "ProdutoCliente",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoCliente",
                table: "ProdutoCliente",
                column: "IdCliente");
        }
    }
}
