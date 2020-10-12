using SoccerShirts.Context;
using SoccerShirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private CarrinhoCompra _carrinhoCompra;

        public PedidoRepository (AppDbContext appDbContext,
            CarrinhoCompra carrinhoCompra)
            
            {
                _appDbContext = appDbContext;
                _carrinhoCompra = carrinhoCompra;
            }




    public void CriarPedido(Pedido pedido)
    {
            pedido.PedidoEnviado = DateTime.Now;
            //pedido.PedidoEntregueEm = DateTime.Now;

            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoDeCompraItems;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    CamisaId = carrinhoItem.Camisa.CamisaId,
                    PedidoId = pedido.PedidoId,
                    Preco = (decimal) carrinhoItem.Camisa.Preco
                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }
            _appDbContext.SaveChanges();
        }
    }
}

