
namespace ShopTFTEC.WebApp.Models;

public class ProdutoListViewModel
{
    public IEnumerable<ProductViewModel> Produtos { get; set; }
    public string CategoriaAtual { get; set; }
}
