namespace Control_Estoque.Models
{
    public class EstoqueProdutosViewModel
    {
        public int EstoqueId { get; set; }
        public string NomeEstoque { get; set; }
        public bool EstoqueAtivo { get; set; }

        public List<ProdutosViewModel> Produtos { get; set; }
        
    }
}
