using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public partial class Cliente
{   [Key]
    public int IdCliente { get; set; }

    public string NomeCliente { get; set; } = null!;

    public string EstadoCliente { get; set; } = null!;

    public string TelefoneCliente {get; set; } = null!;

    public string EnderecoCliente { get; set; } = null!;

    public virtual ICollection<ProdutoCliente> ProdutoCliente { get; set; } = new List<ProdutoCliente>();
}
