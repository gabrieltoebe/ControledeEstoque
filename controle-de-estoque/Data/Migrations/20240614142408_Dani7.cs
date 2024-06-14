using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class Dani7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCliente = table.Column<string>(type: "TEXT", nullable: false),
                    EstadoCliente = table.Column<string>(type: "TEXT", nullable: false),
                    TelefoneCliente = table.Column<string>(type: "TEXT", nullable: false),
                    EnderecoCliente = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoCliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "INTEGER", nullable: false),
                    IdProdFornRec = table.Column<int>(type: "INTEGER", nullable: false),
                    CodProduto = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    CpfId = table.Column<string>(type: "TEXT", nullable: true),
                    Qtde = table.Column<int>(type: "INTEGER", nullable: false),
                    DataRecebimento = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoCliente", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_ProdutoCliente_AspNetUsers_CpfId",
                        column: x => x.CpfId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProdutoCliente_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoCliente_Estoque_IdEstoque",
                        column: x => x.IdEstoque,
                        principalTable: "Estoque",
                        principalColumn: "IdEstoque",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoCliente_Produto_CodProduto",
                        column: x => x.CodProduto,
                        principalTable: "Produto",
                        principalColumn: "CodProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCliente_CodProduto",
                table: "ProdutoCliente",
                column: "CodProduto");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCliente_CpfId",
                table: "ProdutoCliente",
                column: "CpfId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCliente_IdEstoque",
                table: "ProdutoCliente",
                column: "IdEstoque");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoCliente");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
