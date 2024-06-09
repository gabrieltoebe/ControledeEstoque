using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public class ProdutoFornecedorReceb
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdProdFornRec { get; set; }

    //[Key]
    //[Column(Order = 0)]
    public int IdFornecedor { get; set; }
    public virtual required Fornecedor Fornecedor { get; set; } = null!;

    //[Key]
   // [Column(Order = 1)]
    public int CodProduto { get; set; }
    public virtual required Produto Produto { get; set; } = null!;

    //[Key]
  //  [Column(Order = 2)]
    public int IdEstoque { get; set; }
    public virtual required Estoque Estoque { get; set; } = null!;

    public ApplicationUser Cpf { get; set; } = null!;

    public int Qtde { get; set; }

    public DateTime DataRecebimento { get; set; }
}
