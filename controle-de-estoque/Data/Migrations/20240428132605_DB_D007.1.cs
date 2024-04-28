using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class DB_D0071 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfId",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_CpfId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "CpfId",
                table: "Inventario");

            migrationBuilder.AddColumn<int>(
                name: "InventarioIdInv",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InventarioIdInv",
                table: "AspNetUsers",
                column: "InventarioIdInv");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Inventario_InventarioIdInv",
                table: "AspNetUsers",
                column: "InventarioIdInv",
                principalTable: "Inventario",
                principalColumn: "IdInv");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Inventario_InventarioIdInv",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InventarioIdInv",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InventarioIdInv",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CpfId",
                table: "Inventario",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_CpfId",
                table: "Inventario",
                column: "CpfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfId",
                table: "Inventario",
                column: "CpfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
