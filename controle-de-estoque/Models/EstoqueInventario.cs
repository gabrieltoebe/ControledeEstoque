using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public class EstoqueInventario
{
    [Key]
    [Column(Order = 0)]
    public int IdEstoque { get; set; }
    public virtual required Estoque Estoque { get; set; } = null!;

    [Key]
    [Column(Order = 1)]
    public int IdInv { get; set; }
    public virtual required Inventario Inventario { get; set; } = null!;


}