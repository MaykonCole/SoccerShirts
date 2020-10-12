using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Models
{
    public class CarrinhoDeCompraItem
    {
        public int CarrinhoDeCompraItemId { get; set; }
        public Camisa Camisa { get; set; }
        public int Quantidade { get; set; }
        [StringLength(200)]
        public string CarrinhoDeCompraId { get; set; }
    }
}
