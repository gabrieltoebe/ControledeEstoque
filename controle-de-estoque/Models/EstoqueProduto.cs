using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;
public enum TipoMovE
{

    Entrada = 0,
    Saida = 1
}


public class EstoqueProduto
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //public int IdEstProd { get; set; }


    [Key]
    [Column(Order = 0)]
    public int EstoqueId { get; set; }
    public virtual required Estoque Estoque { get; set; } = null!;

    [Key]
    [Column(Order = 1)]
    public int CodProduto { get; set; }
    public virtual required Produto Produto { get; set; } = null!;

    public ApplicationUser Cpf { get; set; } = null!;

    public int Qtde { get; set; }


    [EnumDataType(typeof(TipoMovE))]
    public TipoMovE TipoMovE { get; set; }

}
