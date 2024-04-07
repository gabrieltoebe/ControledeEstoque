using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    /// <inheritdoc />
    public partial class Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "TEXT",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "TEXT",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    IdEstoque = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeEstoque = table.Column<string>(type: "TEXT", nullable: false),
                    TipoEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    AtivEstoque = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.IdEstoque);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    IdFornecedor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeFornecedor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.IdFornecedor);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    CodProduto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeProduto = table.Column<string>(type: "TEXT", nullable: false),
                    Descrição = table.Column<string>(type: "TEXT", nullable: false),
                    CpfId = table.Column<string>(type: "TEXT", nullable: true),
                    DataCadastroProd = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.CodProduto);
                    table.ForeignKey(
                        name: "FK_Produto_AspNetUsers_CpfId",
                        column: x => x.CpfId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    IdInv = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdMovimento = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    CpfId = table.Column<string>(type: "TEXT", nullable: false),
                    TipoMov = table.Column<int>(type: "INTEGER", nullable: false),
                    DataMov = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CpfNavigationId = table.Column<string>(type: "TEXT", nullable: false),
                    IdEstoqueNavigationIdEstoque = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.IdInv);
                    table.ForeignKey(
                        name: "FK_Inventario_AspNetUsers_CpfId",
                        column: x => x.CpfId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventario_AspNetUsers_CpfNavigationId",
                        column: x => x.CpfNavigationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventario_Estoque_IdEstoqueNavigationIdEstoque",
                        column: x => x.IdEstoqueNavigationIdEstoque,
                        principalTable: "Estoque",
                        principalColumn: "IdEstoque",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstoqueProduto",
                columns: table => new
                {
                    EstoqueId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodProduto = table.Column<int>(type: "INTEGER", nullable: false),
                    Qtde = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstoqueProduto", x => new { x.EstoqueId, x.CodProduto });
                    table.ForeignKey(
                        name: "FK_EstoqueProduto_Estoque_EstoqueId",
                        column: x => x.EstoqueId,
                        principalTable: "Estoque",
                        principalColumn: "IdEstoque",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstoqueProduto_Produto_CodProduto",
                        column: x => x.CodProduto,
                        principalTable: "Produto",
                        principalColumn: "CodProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoFornecedorReceb",
                columns: table => new
                {
                    IdFornecedor = table.Column<int>(type: "INTEGER", nullable: false),
                    CodProduto = table.Column<int>(type: "INTEGER", nullable: false),
                    Qtde = table.Column<int>(type: "INTEGER", nullable: false),
                    DataRecebimento = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoFornecedorReceb", x => new { x.IdFornecedor, x.CodProduto });
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedorReceb_Fornecedor_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "Fornecedor",
                        principalColumn: "IdFornecedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedorReceb_Produto_CodProduto",
                        column: x => x.CodProduto,
                        principalTable: "Produto",
                        principalColumn: "CodProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventarioProduto",
                columns: table => new
                {
                    IdInv = table.Column<int>(type: "INTEGER", nullable: false),
                    CodProduto = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioProduto", x => new { x.IdInv, x.CodProduto });
                    table.ForeignKey(
                        name: "FK_InventarioProduto_Inventario_IdInv",
                        column: x => x.IdInv,
                        principalTable: "Inventario",
                        principalColumn: "IdInv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventarioProduto_Produto_CodProduto",
                        column: x => x.CodProduto,
                        principalTable: "Produto",
                        principalColumn: "CodProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstoqueProduto_CodProduto",
                table: "EstoqueProduto",
                column: "CodProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_CpfId",
                table: "Inventario",
                column: "CpfId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_CpfNavigationId",
                table: "Inventario",
                column: "CpfNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_IdEstoqueNavigationIdEstoque",
                table: "Inventario",
                column: "IdEstoqueNavigationIdEstoque");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProduto_CodProduto",
                table: "InventarioProduto",
                column: "CodProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CpfId",
                table: "Produto",
                column: "CpfId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedorReceb_CodProduto",
                table: "ProdutoFornecedorReceb",
                column: "CodProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstoqueProduto");

            migrationBuilder.DropTable(
                name: "InventarioProduto");

            migrationBuilder.DropTable(
                name: "ProdutoFornecedorReceb");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");
        }
    }
}
