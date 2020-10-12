using SoccerShirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Repositories
{
   public  interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
