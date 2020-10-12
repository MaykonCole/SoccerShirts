using Microsoft.AspNetCore.Mvc;
using SoccerShirts.Models;
using SoccerShirts.Repositories;
using SoccerShirts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Controllers
{
    public class CamisaController : Controller
    {
        private readonly ICamisaRepository _camisaRepository;
        private ICategoriaRepository _categoriaRepository;

        public CamisaController(ICamisaRepository camisaRepository, ICategoriaRepository categoriaRepository)
        {
            _camisaRepository = camisaRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult ListarCamisas(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Camisa> camisas;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria) || string.Equals("Todas as camisas", _categoria, StringComparison.OrdinalIgnoreCase))
            {
                camisas = _camisaRepository.Camisa.OrderBy(c => c.CamisaId);
                categoria = "Todas as camisas";
            }
            else
            {
                if (string.Equals("Nacional", _categoria, StringComparison.OrdinalIgnoreCase))
                {
                    camisas = _camisaRepository.Camisa.Where(c => c.Categoria.CategoriaNome.Equals("Nacional")).OrderBy(c => c.Nome);
                }

                else if (string.Equals("Internacional", _categoria, StringComparison.OrdinalIgnoreCase))
                {
                    camisas = _camisaRepository.Camisa.Where(c => c.Categoria.CategoriaNome.Equals("Internacional")).OrderBy(c => c.Nome);
                }

                else
                {
                    camisas = _camisaRepository.Camisa.Where(c => c.Categoria.CategoriaNome.Equals("Seleção")).OrderBy(c => c.Nome);
                }

                categoriaAtual = _categoria;
            }

            var camisasListViewModel = new CamisaListViewModel
            {
                Camisas = camisas,
                CategoriaAtual = categoriaAtual

            };
            return View (camisasListViewModel);
        }

        public IActionResult Details (int camisaId)
        {
            var camisa = _camisaRepository.Camisa.FirstOrDefault(c => c.CamisaId == camisaId);
            if (camisa == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            else
            {
                return View(camisa);
            }
        }


        public IActionResult Search (string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Camisa> camisas;
            string _categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                camisas = _camisaRepository.Camisa.OrderBy(c => c.CamisaId);
            }
            else
            {
                camisas = _camisaRepository.Camisa.Where(c => c.Nome.ToLower().Contains(_searchString));
            }

            return View("~/Views/Camisa/ListarCamisas.cshtml", new CamisaListViewModel { Camisas = camisas, CategoriaAtual = "Todas as camisas" });

        }
    }
}
