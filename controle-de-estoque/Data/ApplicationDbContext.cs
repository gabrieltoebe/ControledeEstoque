using Control_Estoque.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Control_Estoque.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);


        modelBuilder.Entity<EstoqueProduto>()
            .HasKey(ep => new { ep.IdEstProd });

        modelBuilder.Entity<EstoqueProduto>()
            .HasOne(ep => ep.Estoque)
            .WithMany(e => e.EstoqueProdutos)
            .HasForeignKey(ep => ep.EstoqueId);

        modelBuilder.Entity<EstoqueProduto>()
            .HasOne(ep => ep.Produto)
            .WithMany(p => p.EstoqueProdutos)
            .HasForeignKey(ep => ep.CodProduto);


        modelBuilder.Entity<ProdutoFornecedorReceb>()
            .HasKey(pfr => new { pfr.IdProdFornRec });

        modelBuilder.Entity<ProdutoFornecedorReceb>()
            .HasOne(pfr => pfr.Fornecedor)
            .WithMany(f => f.ProdutoFornecedorRecebs)
            .HasForeignKey(pfr => pfr.IdFornecedor);

        modelBuilder.Entity<ProdutoFornecedorReceb>()
            .HasOne(pfr => pfr.Produto)
            .WithMany(p => p.ProdutoFornecedorRecebs)
            .HasForeignKey(pfr => pfr.CodProduto);

        modelBuilder.Entity<ProdutoFornecedorReceb>()
           .HasOne(pfr => pfr.Estoque)
           .WithMany(p => p.ProdutoFornecedorRecebs)
           .HasForeignKey(pfr => pfr.IdEstoque);


        modelBuilder.Entity<InventarioProduto>()
            .HasKey(ip => new { ip.IdInvProd });

        modelBuilder.Entity<InventarioProduto>()
            .HasOne(ip => ip.Inventario)
            .WithMany(i => i.InventarioProdutos)
           .HasForeignKey(ip => ip.IdInv);

        modelBuilder.Entity<InventarioProduto>()
            .HasOne(ip => ip.Produto)
            .WithMany(p => p.InventarioProdutos)
            .HasForeignKey(ip => ip.CodProduto);




    }
    public DbSet<Control_Estoque.Models.Produto> Produto { get; set; } = default!;

    public DbSet<Control_Estoque.Models.Fornecedor> Fornecedor { get; set; } = default!;

    public DbSet<Control_Estoque.Models.Estoque> Estoque { get; set; } = default!;

    public DbSet<Control_Estoque.Models.Inventario> Inventario { get; set; } = default!;

    public DbSet<Control_Estoque.Models.EstoqueProduto> EstoqueProduto { get; set; } = default!;

    public DbSet<Control_Estoque.Models.ProdutoFornecedorReceb> ProdutoFornecedorReceb { get; set; } = default!;

    public DbSet<Control_Estoque.Models.InventarioProduto> InventarioProduto { get; set; } = default!;
}
