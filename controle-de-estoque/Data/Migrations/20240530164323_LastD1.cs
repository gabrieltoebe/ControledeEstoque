using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class LastD1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Estoque_IdEstoque",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_IdEstoque",
                table: "Inventario");

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
    }
}
