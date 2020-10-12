using Microsoft.EntityFrameworkCore;
using SoccerShirts.Context;
using SoccerShirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Repositories
{
    public class CamisaRepository : ICamisaRepository
    {
        private readonly AppDbContext _context;

        public CamisaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Camisa> Camisa => _context.Camisas.Include(c => c.Categoria);

        public IEnumerable<Camisa> CamisaMaisVendidas => _context.Camisas.Where(mv => mv.IsCamisaMaisVendida).Include(c => c.Categoria);

        public Camisa GetCamisaById(int camisaId)
        {
            return _context.Camisas.FirstOrDefault(c => c.CamisaId == camisaId);
        }
    }
}
