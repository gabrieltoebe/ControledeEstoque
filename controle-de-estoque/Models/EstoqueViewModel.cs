using Microsoft.AspNetCore.Mvc.Rendering;

namespace Control_Estoque.Models
{
    public class ProdutoViewModel
    {
        public int SelectedEstoqueId { get; set; }
        public IEnumerable<SelectListItem> EstoqueItems { get; set; }
    }
}