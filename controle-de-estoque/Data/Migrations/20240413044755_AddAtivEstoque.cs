using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAtivEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfId",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfNavigationId",
                table: "Inventario");

            migrationBuilder.AlterColumn<string>(
                name: "CpfNavigationId",
                table: "Inventario",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "CpfId",
                table: "Inventario",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "AtivEstoque",
                table: "Estoque",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfId",
                table: "Inventario",
                column: "CpfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfNavigationId",
                table: "Inventario",
                column: "CpfNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfId",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfNavigationId",
                table: "Inventario");

            migrationBuilder.AlterColumn<string>(
                name: "CpfNavigationId",
                table: "Inventario",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CpfId",
                table: "Inventario",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "AtivEstoque",
                table: "Estoque",
                type: "BLOB",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfId",
                table: "Inventario",
                column: "CpfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_AspNetUsers_CpfNavigationId",
                table: "Inventario",
                column: "CpfNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
