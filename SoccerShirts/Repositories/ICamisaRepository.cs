using SoccerShirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Repositories
{
    public interface ICamisaRepository
    {
        // Coleção de Camisas
        IEnumerable<Camisa> Camisa { get; }
        // Coleção de Camisas mais vendidas
        IEnumerable<Camisa> CamisaMaisVendidas { get; }
        // Retornar camisa pelo ID
        Camisa GetCamisaById(int camisaId);
    }
}
