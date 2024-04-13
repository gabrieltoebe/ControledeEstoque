using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public enum TipoMov
{
    entrada,
    saida
}

public partial class Inventario
{
    [Key]
    public int IdInv { get; set; }

    public int IdEstoque { get; set; }

    public ApplicationUser Cpf { get; set; } = null!;

    [EnumDataType(typeof(TipoMov))]
    public TipoMov TipoMov { get; set; }

    public DateOnly DataMov { get; set; }

    public virtual ICollection<InventarioProduto> InventarioProdutos { get; set; } = new List<InventarioProduto>();

    public virtual ApplicationUser CpfNavigation { get; set; } = null!;

    public virtual Estoque IdEstoqueNavigation { get; set; } = null!;
}
