namespace Control_Estoque.Models
{
    public class ProdutosViewModel
    {
        public int CodProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public int QuantidadeNoEstoque { get; set; }
        public int QuantidadeMinima { get; set; }
        public int QuantidadeMaxima { get; set; }
        public string UnidadeMed { get; set; }
    }
}
