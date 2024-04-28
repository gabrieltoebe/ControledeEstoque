using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class DB_D0073 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstoqueInventario");

            migrationBuilder.AddColumn<int>(
                name: "EstoqueIdEstoque",
                table: "Inventario",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEstoque",
                table: "Inventario",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "IdEstoque",
                table: "Inventario");

            migrationBuilder.CreateTable(
                name: "EstoqueInventario",
                columns: table => new
                {
                    IdEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    InventariosIdInv = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstoqueInventario", x => new { x.IdEstoque, x.InventariosIdInv });
                    table.ForeignKey(
                        name: "FK_EstoqueInventario_Estoque_IdEstoque",
                        column: x => x.IdEstoque,
                        principalTable: "Estoque",
                        principalColumn: "IdEstoque",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstoqueInventario_Inventario_InventariosIdInv",
                        column: x => x.InventariosIdInv,
                        principalTable: "Inventario",
                        principalColumn: "IdInv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstoqueInventario_InventariosIdInv",
                table: "EstoqueInventario",
                column: "InventariosIdInv");
        }
    }
}
