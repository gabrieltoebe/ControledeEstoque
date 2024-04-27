using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public partial class Produto
{
    [Key]
    public int CodProduto { get; set; }

    public string NomeProduto { get; set; } = null!;

    public string Descrição { get; set; } = null!;
    public int EstoqueMinimo { get; set; } 
    //public string? IdEstoque { get; set; } 
    public int ValidadeDias { get; set; } 
    public string UnidadeMedida { get; set; } = null!;

    public ApplicationUser Cpf { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime DataCadastroProd { get; set; }

    public virtual ICollection<InventarioProduto> InventarioProdutos { get; set; } = new List<InventarioProduto>();

    public virtual ICollection<ProdutoFornecedorReceb> ProdutoFornecedorRecebs { get; set; } = new List<ProdutoFornecedorReceb>();

    public virtual ICollection<EstoqueProduto> EstoqueProdutos { get; set; } = new List<EstoqueProduto>();

    public virtual ICollection<Estoque> IdEstoque { get; set; } = new List<Estoque>();
}
