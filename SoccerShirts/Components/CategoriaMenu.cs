using Microsoft.AspNetCore.Mvc;
using SoccerShirts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoria)
        {
            _categoriaRepository = categoria;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categoria.OrderBy(c => c.CategoriaNome);

            return View(categorias);
        }

    }
}
