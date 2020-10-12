using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerShirts.Migrations
{
    public partial class PopularTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Populando Categorias
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao) VALUES ('Nacional', 'Camisas dos 20 Clubes da SÉRIE A.')" );
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao) VALUES ('Internacional', 'Camisas dos principais clubes internacionais.')");
           migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao) VALUES ('Seleção', 'Camisas das principais seleções do mundo.')");

            // Populando Camisas
            migrationBuilder.Sql("INSERT INTO Camisas (Nome, DescricaoCurta, DescricaoDestalhada, Preco, ImagemURL, ImagemMiniatura, IsCamisaMaisVendida, EmEstoque, CategoriaID) values ('Atlético Mineiro', 'Tecido DryFit', 'Camisa DryFit, Gola Polo, Oficial', '229.90', 'encurtador.com.br/aloFG', 'encurtador.com.br/aloFG', 1, 1,(select CategoriaId from categorias where CategoriaNome = 'Nacional'))");
            migrationBuilder.Sql("INSERT INTO Camisas (Nome, DescricaoCurta, DescricaoDestalhada, Preco, ImagemURL, ImagemMiniatura, IsCamisaMaisVendida, EmEstoque, CategoriaID) values ('Arsenal', 'Tecido DryFitMax', 'Camisa DryFit, Adidas, Gola Polo, Oficial', '269.90', 'encurtador.com.br/nzUZ8', 'encurtador.com.br/nzUZ8', 1, 1, (select CategoriaId from categorias where CategoriaNome = 'Internacional'))");
            migrationBuilder.Sql("INSERT INTO Camisas (Nome, DescricaoCurta, DescricaoDestalhada, Preco, ImagemURL, ImagemMiniatura, IsCamisaMaisVendida, EmEstoque, CategoriaID) values ('Alemanha', 'Tecido DryFitMax', 'Camisa DryFit, Adidas, Gola Polo, Oficial', '249.90', 'encurtador.com.br/jvPRY', 'encurtador.com.br/jvPRY', 1, 1,(select CategoriaId from categorias where CategoriaNome = 'Seleção'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Camisas");
        }
    }
}
