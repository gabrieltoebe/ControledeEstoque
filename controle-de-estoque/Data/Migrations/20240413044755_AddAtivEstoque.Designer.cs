﻿// <auto-generated />
using System;
using Control_Estoque.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Control_Estoque.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240413044755_AddAtivEstoque")]
    partial class AddAtivEstoque
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Control_Estoque.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Control_Estoque.Models.Estoque", b =>
                {
                    b.Property<int>("IdEstoque")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AtivEstoque")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomeEstoque")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TipoEstoque")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdEstoque");

                    b.ToTable("Estoque");
                });

            modelBuilder.Entity("Control_Estoque.Models.EstoqueProduto", b =>
                {
                    b.Property<int>("EstoqueId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CodProduto")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Qtde")
                        .HasColumnType("INTEGER");

                    b.HasKey("EstoqueId", "CodProduto");

                    b.HasIndex("CodProduto");

                    b.ToTable("EstoqueProduto");
                });

            modelBuilder.Entity("Control_Estoque.Models.Fornecedor", b =>
                {
                    b.Property<int>("IdFornecedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomeFornecedor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdFornecedor");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("Control_Estoque.Models.Inventario", b =>
                {
                    b.Property<int>("IdInv")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CpfId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CpfNavigationId")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DataMov")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdEstoque")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdEstoqueNavigationIdEstoque")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdMovimento")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TipoMov")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdInv");

                    b.HasIndex("CpfId");

                    b.HasIndex("CpfNavigationId");

                    b.HasIndex("IdEstoqueNavigationIdEstoque");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("Control_Estoque.Models.InventarioProduto", b =>
                {
                    b.Property<int>("IdInv")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CodProduto")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdInv", "CodProduto");

                    b.HasIndex("CodProduto");

                    b.ToTable("InventarioProduto");
                });

            modelBuilder.Entity("Control_Estoque.Models.Produto", b =>
                {
                    b.Property<int>("CodProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CpfId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastroProd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descrição")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CodProduto");

                    b.HasIndex("CpfId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Control_Estoque.Models.ProdutoFornecedorReceb", b =>
                {
                    b.Property<int>("IdFornecedor")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CodProduto")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DataRecebimento")
                        .HasColumnType("TEXT");

                    b.Property<int>("Qtde")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdFornecedor", "CodProduto");

                    b.HasIndex("CodProduto");

                    b.ToTable("ProdutoFornecedorReceb");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Control_Estoque.Models.EstoqueProduto", b =>
                {
                    b.HasOne("Control_Estoque.Models.Produto", "Produto")
                        .WithMany("EstoqueProdutos")
                        .HasForeignKey("CodProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Control_Estoque.Models.Estoque", "Estoque")
                        .WithMany("EstoqueProdutos")
                        .HasForeignKey("EstoqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estoque");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Control_Estoque.Models.Inventario", b =>
                {
                    b.HasOne("Control_Estoque.Models.ApplicationUser", "Cpf")
                        .WithMany()
                        .HasForeignKey("CpfId");

                    b.HasOne("Control_Estoque.Models.ApplicationUser", "CpfNavigation")
                        .WithMany()
                        .HasForeignKey("CpfNavigationId");

                    b.HasOne("Control_Estoque.Models.Estoque", "IdEstoqueNavigation")
                        .WithMany("Inventarios")
                        .HasForeignKey("IdEstoqueNavigationIdEstoque")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cpf");

                    b.Navigation("CpfNavigation");

                    b.Navigation("IdEstoqueNavigation");
                });

            modelBuilder.Entity("Control_Estoque.Models.InventarioProduto", b =>
                {
                    b.HasOne("Control_Estoque.Models.Produto", "Produto")
                        .WithMany("InventarioProdutos")
                        .HasForeignKey("CodProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Control_Estoque.Models.Inventario", "Inventario")
                        .WithMany("InventarioProdutos")
                        .HasForeignKey("IdInv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventario");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Control_Estoque.Models.Produto", b =>
                {
                    b.HasOne("Control_Estoque.Models.ApplicationUser", "Cpf")
                        .WithMany()
                        .HasForeignKey("CpfId");

                    b.Navigation("Cpf");
                });

            modelBuilder.Entity("Control_Estoque.Models.ProdutoFornecedorReceb", b =>
                {
                    b.HasOne("Control_Estoque.Models.Produto", "Produto")
                        .WithMany("ProdutoFornecedorRecebs")
                        .HasForeignKey("CodProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Control_Estoque.Models.Fornecedor", "Fornecedor")
                        .WithMany("ProdutoFornecedorRecebs")
                        .HasForeignKey("IdFornecedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Control_Estoque.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Control_Estoque.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Control_Estoque.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Control_Estoque.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Control_Estoque.Models.Estoque", b =>
                {
                    b.Navigation("EstoqueProdutos");

                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("Control_Estoque.Models.Fornecedor", b =>
                {
                    b.Navigation("ProdutoFornecedorRecebs");
                });

            modelBuilder.Entity("Control_Estoque.Models.Inventario", b =>
                {
                    b.Navigation("InventarioProdutos");
                });

            modelBuilder.Entity("Control_Estoque.Models.Produto", b =>
                {
                    b.Navigation("EstoqueProdutos");

                    b.Navigation("InventarioProdutos");

                    b.Navigation("ProdutoFornecedorRecebs");
                });
#pragma warning restore 612, 618
        }
    }
}
