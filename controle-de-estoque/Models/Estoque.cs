using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public partial class Estoque
{
    [Key]
    public int IdEstoque { get; set; }

    public string NomeEstoque { get; set; } = null!;

    public int TipoEstoque { get; set; }
    public int QuantidadeDeItensNoEstoque { get; set; }

    public bool AtivEstoque { get; set; }
    public ApplicationUser Cpf { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<EstoqueProduto>? EstoqueProdutos { get; set; }
    public virtual ICollection<ProdutoFornecedorReceb>? ProdutoFornecedorRecebs { get; set; }
    public virtual ICollection<Produto>? Produtos { get; set; }
}
