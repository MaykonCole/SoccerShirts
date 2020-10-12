using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Models
{
    public class Camisa
    {
        // Chave Primária
        public int CamisaId { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(100)]
        public string DescricaoCurta { get; set; }
        [StringLength(250)]
        public string DescricaoDestalhada { get; set; }
        [Column (TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        [StringLength(200)]
        public string ImagemUrl { get; set; }
        [StringLength(200)]
        public string ImagemMiniatura { get; set; }
        public bool IsCamisaMaisVendida { get; set; }
        public bool EmEstoque { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }



    }
}
