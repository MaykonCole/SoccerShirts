using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Models
{
    public class Categoria
    {
        // Chave Primária
        public int CategoriaId { get; set; }
        [StringLength(100)]
        public string CategoriaNome { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }
        public List<Camisa> Camisas { get; set; }

    }
}
