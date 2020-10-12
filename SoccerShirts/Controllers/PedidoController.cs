
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoccerShirts.Models;
using SoccerShirts.Repositories;

namespace SoccerShirts.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository,
            CarrinhoCompra carrinhoCompra)
        
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

       public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido p)
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoDeCompraItems = itens;

            if (_carrinhoCompra.CarrinhoDeCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio, inclua algum item ...");
            }
            
            if (ModelState.IsValid)

            {
                _pedidoRepository.CriarPedido(p);

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :) ";

                ViewBag.TotalPedido = _carrinhoCompra.GetValorCarrinhoCompraTotal();

                _carrinhoCompra.LimparCarrinho();
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", p);
            }

            return View(p);
        }

        public IActionResult CheckoutCompleto()
        {
            ViewBag.Cliente = TempData["Cliente"];
            ViewBag.DataPedido = TempData["DataPedido"];
            ViewBag.NumeroPedido = TempData["NumeroPedido"];
            ViewBag.TotalPedido = TempData["TotalPedido"];
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :) ";
            return View();
        }
    }
}
