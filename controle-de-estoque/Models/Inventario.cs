using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public enum TipoMov
{
    Ambos,
    Entrada,
    Saida
}

public partial class Inventario
{
    [Key]
    public int IdInv { get; set; }

    //public int IdEstoque { get; set; }

    //public ApplicationUser Cpf { get; set; } = null!;


    [EnumDataType(typeof(TipoMov))]
    public TipoMov TipoMov { get; set; }

    public DateOnly DataMov { get; set; }

    public virtual ICollection<InventarioProduto> InventarioProdutos { get; set; } = new List<InventarioProduto>();

    public virtual ApplicationUser Cpf { get; set; } = null!;

    public virtual ICollection<Estoque>  IdEstoque { get; set; } = new List<Estoque>() ;
}
