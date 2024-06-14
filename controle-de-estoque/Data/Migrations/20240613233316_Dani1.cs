using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class Dani1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EstoqueProduto",
                table: "EstoqueProduto");

            migrationBuilder.AlterColumn<int>(
                name: "CodProduto",
                table: "EstoqueProduto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "EstoqueId",
                table: "EstoqueProduto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEstProd",
                table: "EstoqueProduto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "TipoMov",
                table: "EstoqueProduto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstoqueProduto",
                table: "EstoqueProduto",
                column: "IdEstProd");

            migrationBuilder.CreateIndex(
                name: "IX_EstoqueProduto_EstoqueId",
                table: "EstoqueProduto",
                column: "EstoqueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EstoqueProduto",
                table: "EstoqueProduto");

            migrationBuilder.DropIndex(
                name: "IX_EstoqueProduto_EstoqueId",
                table: "EstoqueProduto");

            migrationBuilder.DropColumn(
                name: "IdEstProd",
                table: "EstoqueProduto");

            migrationBuilder.DropColumn(
                name: "TipoMov",
                table: "EstoqueProduto");

            migrationBuilder.AlterColumn<int>(
                name: "EstoqueId",
                table: "EstoqueProduto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "CodProduto",
                table: "EstoqueProduto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstoqueProduto",
                table: "EstoqueProduto",
                columns: new[] { "EstoqueId", "CodProduto" });
        }
    }
}
