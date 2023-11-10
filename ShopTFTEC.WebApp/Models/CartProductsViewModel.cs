namespace ShopTFTEC.WebApp.Models
{
    public class CartProductsViewModel
    {
        public CartViewModel Pedido { get; set; }
        public IEnumerable<CartItemViewModel> PedidoDetalhes { get; set; }
    }
}
