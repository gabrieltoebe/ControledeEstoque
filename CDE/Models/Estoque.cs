using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public partial class Estoque
{
    [Key]
    public int IdEstoque { get; set; }

    public string NomeEstoque { get; set; } = null!;

    public int TipoEstoque { get; set; }

    public bool AtivEstoque { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<EstoqueProduto>? EstoqueProdutos { get; set; }
}
