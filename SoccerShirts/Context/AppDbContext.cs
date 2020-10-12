using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoccerShirts.Models;


namespace SoccerShirts.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
                
        }

        //Tabelas do meu BD
        public DbSet<Camisa> Camisas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarrinhoDeCompraItem> CarrinhoCompraDeItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }

    }
}
