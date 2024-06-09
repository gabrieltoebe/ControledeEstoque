using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class modif5 : Migration
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
                .OldAnnotation("Relational:ColumnOrder", 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdEstoque",
                table: "ProdutoFornecedorReceb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 2);
        }
    }
}
