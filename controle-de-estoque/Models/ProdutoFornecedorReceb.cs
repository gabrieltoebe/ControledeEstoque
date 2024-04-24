using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public class ProdutoFornecedorReceb
{
    [Key]
    [Column(Order = 0)]
    public int IdFornecedor { get; set; }
    public virtual required Fornecedor Fornecedor { get; set; } = null!;

    [Key]
    [Column(Order = 1)]
    public int CodProduto { get; set; }
    public virtual required Produto Produto { get; set; } = null!;

    public int Qtde { get; set; }

    public DateOnly DataRecebimento { get; set; }
}
