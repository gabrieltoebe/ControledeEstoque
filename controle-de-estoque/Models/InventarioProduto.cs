namespace Control_Estoque.Models;

public class InventarioProduto
{
    public int IdInv { get; set; }
    public virtual Inventario Inventario { get; set; } = null!;

    public int CodProduto { get; set; }
    public virtual Produto Produto { get; set; } = null!;

    public int Quantidade { get; set; }
}