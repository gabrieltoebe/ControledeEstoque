using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class DB_D002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfNavigationId",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Estoque_IdEstoqueNavigationIdEstoque",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_CpfNavigationId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "CpfNavigationId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "IdEstoque",
                table: "Inventario");

            migrationBuilder.RenameColumn(
                name: "IdEstoqueNavigationIdEstoque",
                table: "Inventario",
                newName: "IdEstoque1");

            migrationBuilder.RenameIndex(
                name: "IX_Inventario_IdEstoqueNavigationIdEstoque",
                table: "Inventario",
                newName: "IX_Inventario_IdEstoque1");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Estoque_IdEstoque1",
                table: "Inventario",
                column: "IdEstoque1",
                principalTable: "Estoque",
                principalColumn: "IdEstoque",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Estoque_IdEstoque1",
                table: "Inventario");

            migrationBuilder.RenameColumn(
                name: "IdEstoque1",
                table: "Inventario",
                newName: "IdEstoqueNavigationIdEstoque");

            migrationBuilder.RenameIndex(
                name: "IX_Inventario_IdEstoque1",
                table: "Inventario",
                newName: "IX_Inventario_IdEstoqueNavigationIdEstoque");

            migrationBuilder.AddColumn<string>(
                name: "CpfNavigationId",
                table: "Inventario",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEstoque",
                table: "Inventario",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_CpfNavigationId",
                table: "Inventario",
                column: "CpfNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfNavigationId",
                table: "Inventario",
                column: "CpfNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Estoque_IdEstoqueNavigationIdEstoque",
                table: "Inventario",
                column: "IdEstoqueNavigationIdEstoque",
                principalTable: "Estoque",
                principalColumn: "IdEstoque",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
