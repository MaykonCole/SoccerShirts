using SoccerShirts.Context;
using SoccerShirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public IEnumerable<Categoria> Categoria => _context.Categorias;
    }
}
