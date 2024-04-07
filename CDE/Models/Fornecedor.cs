using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public partial class Fornecedor
{   [Key]
    public int IdFornecedor { get; set; }

    public string NomeFornecedor { get; set; } = null!;

    public virtual ICollection<ProdutoFornecedorReceb> ProdutoFornecedorRecebs { get; set; } = new List<ProdutoFornecedorReceb>();
}
