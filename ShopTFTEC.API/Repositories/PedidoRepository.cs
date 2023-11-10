using ShopTFTEC.API.Context;
using ShopTFTEC.API.Models;

namespace ShopTFTEC.API.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly Cart _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext,
            Cart carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CartItems;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantity,
                    ProdutoId = carrinhoItem.Product.Id,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Product.Price
                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }
            _appDbContext.SaveChanges();
        }
    }
}
