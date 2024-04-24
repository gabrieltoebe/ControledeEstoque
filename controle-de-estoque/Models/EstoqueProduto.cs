using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public class EstoqueProduto
{
    [Key]
    [Column(Order = 0)]
    public int EstoqueId { get; set; }
    public virtual required Estoque Estoque { get; set; }

    [Key]
    [Column(Order = 1)]
    public int CodProduto { get; set; }
    public virtual required Produto Produto { get; set; }

    public int Qtde { get; set; }

}
