using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NuGet.Packaging.PackagingConstants;

namespace Control_Estoque.Models;


public partial class Inventario
{
    [Key]
    public int IdInv { get; set; }
   
    public int IdEstoque { get; set; }

    public ApplicationUser Cpf { get; set; } = null!;
     

    public DateOnly DataMov { get; set; }

    public virtual ICollection<InventarioProduto> InventarioProdutos { get; set; } = new List<InventarioProduto>();


    //public virtual ICollection<ApplicationUser> Cpf { get; set; } = new List<ApplicationUser>();
    [ForeignKey(nameof(IdEstoque))]
    public  Estoque? Estoque { get; set; } 
}
