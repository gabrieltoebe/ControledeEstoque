using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class LastD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstoqueIdEstoque",
                table: "Inventario",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_EstoqueIdEstoque",
                table: "Inventario",
                column: "EstoqueIdEstoque");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Estoque_EstoqueIdEstoque",
                table: "Inventario",
                column: "EstoqueIdEstoque",
                principalTable: "Estoque",
                principalColumn: "IdEstoque");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Estoque_EstoqueIdEstoque",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_EstoqueIdEstoque",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "EstoqueIdEstoque",
                table: "Inventario");
        }
    }
}
