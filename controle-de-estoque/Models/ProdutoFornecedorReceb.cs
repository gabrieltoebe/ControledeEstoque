namespace Control_Estoque.Models;

public class ProdutoFornecedorReceb
{
    public int IdFornecedor { get; set; }
    public virtual Fornecedor Fornecedor { get; set; } = null!;

    public int CodProduto { get; set; }
    public virtual Produto Produto { get; set; } = null!;

    public int Qtde { get; set; }

    public DateOnly DataRecebimento { get; set; }
}
