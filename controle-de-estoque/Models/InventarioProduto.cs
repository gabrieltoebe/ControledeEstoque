using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public class InventarioProduto
{
    [Key]
    [Column(Order = 0)]
    public int IdInv { get; set; }
    public virtual required Inventario Inventario { get; set; } = null!;

    [Key]
    [Column(Order = 1)]
    public int CodProduto { get; set; }
    public virtual required Produto Produto { get; set; } = null!;

    public int Quantidade { get; set; }
}