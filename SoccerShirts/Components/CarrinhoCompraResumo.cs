using Microsoft.AspNetCore.Mvc;
using SoccerShirts.Models;
using SoccerShirts.ViewModels;
using System.Collections.Generic;

namespace SoccerShirts.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
           _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();

            //var itens = new List<CarrinhoDeCompraItem>() { new CarrinhoDeCompraItem(), new CarrinhoDeCompraItem() };

            _carrinhoCompra.CarrinhoDeCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetValorCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }
        
    }
}
