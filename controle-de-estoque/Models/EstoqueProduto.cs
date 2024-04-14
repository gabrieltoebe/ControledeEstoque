namespace Control_Estoque.Models;

public class EstoqueProduto
{
    public int EstoqueId { get; set; }
    public required Estoque Estoque { get; set; }

    public int CodProduto { get; set; }
    public required Produto Produto { get; set; }

    public int Qtde { get; set; }

}
