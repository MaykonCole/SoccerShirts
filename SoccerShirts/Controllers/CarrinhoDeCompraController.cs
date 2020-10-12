using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerShirts.Models;
using SoccerShirts.Repositories;
using SoccerShirts.ViewModels;

namespace SoccerShirts.Controllers
{
    [Authorize]
    public class CarrinhoDeCompraController : Controller
    {
        private readonly ICamisaRepository _camisaRepositoy;
        private CarrinhoCompra _carrinhoCompra;

        public CarrinhoDeCompraController(ICamisaRepository camisaRepository,
            CarrinhoCompra carrinhoCompra)
        {
            _camisaRepositoy = camisaRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        // Exibe o carrinho de Compra
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoDeCompraItems = itens;

            var carrinhoCompraViewModel = new CarrinhoCompraViewModel
            {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetValorCarrinhoCompraTotal()
            };
            return View(carrinhoCompraViewModel);
        }
        // Adiciona um item ao Carrinho e após retorna a página Index
        
        [Authorize]
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra (int camisaId)
        {
            var camisaSelecionada = _camisaRepositoy.Camisa.FirstOrDefault(c => c.CamisaId == camisaId);

            if (camisaSelecionada != null)
            {
                _carrinhoCompra.AdicionarCarrinho(camisaSelecionada, 1);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int camisaId)
        {
            var camisaSelecionada = _camisaRepositoy.Camisa.FirstOrDefault(c => c.CamisaId == camisaId);

            if (camisaSelecionada != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(camisaSelecionada);
            }

            return RedirectToAction("Index");
        }
    }
}
