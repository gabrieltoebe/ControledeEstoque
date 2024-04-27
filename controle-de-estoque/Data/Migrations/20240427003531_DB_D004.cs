using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class DB_D004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Estoque_IdEstoque1",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_IdEstoque1",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "IdEstoque1",
                table: "Inventario");

            migrationBuilder.RenameColumn(
                name: "EstoqueID",
                table: "Produto",
                newName: "IdEstoque");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstoqueInventario");

            migrationBuilder.RenameColumn(
                name: "IdEstoque",
                table: "Produto",
                newName: "EstoqueID");

            migrationBuilder.AddColumn<int>(
                name: "IdEstoque1",
                table: "Inventario",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_IdEstoque1",
                table: "Inventario",
                column: "IdEstoque1");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Estoque_IdEstoque1",
                table: "Inventario",
                column: "IdEstoque1",
                principalTable: "Estoque",
                principalColumn: "IdEstoque",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
