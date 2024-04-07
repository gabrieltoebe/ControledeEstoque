using System.ComponentModel.DataAnnotations;

namespace Control_Estoque.Models;

public partial class Estoque
{
    [Key]
    public int IdEstoque { get; set; }

    public string NomeEstoque { get; set; } = null!;

    public int TipoEstoque { get; set; }

    public byte[] AtivEstoque { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<EstoqueProduto>? EstoqueProdutos { get; set; }
}
