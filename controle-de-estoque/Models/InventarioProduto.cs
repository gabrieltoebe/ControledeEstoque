using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public enum TipoMov
{
   
    Entrada = 0,
    Saida = 1
}


public class InventarioProduto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdInvProd { get; set; }


    // [Key]
    //  [Column(Order = 0)]
    public int IdInv { get; set; }
    public virtual required Inventario Inventario { get; set; } = null!;

   // [Key]
  //  [Column(Order = 1)]
    public int CodProduto { get; set; }
       public virtual required Produto Produto { get; set; } = null!;

    [EnumDataType(typeof(TipoMov))]
    public TipoMov TipoMov { get; set; }

    public int Quantidade { get; set; }

    public ApplicationUser Cpf { get; set; } = null!;

}