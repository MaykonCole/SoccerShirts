using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoccerShirts.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _contexto;

        public CarrinhoCompra(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoDeCompraItem> CarrinhoDeCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //Define uma sessão acessando o contexto atual ( Tem que registrar em IServices)

            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtem um servico do tipo do nosso contexto

            var context = services.GetService<AppDbContext>();

            // Obtem ou gera o ID do carrinho pelo NewGuid que gera um Identificador Único

            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o ID do carrinho na Sessão

            session.SetString("CarrinhoId", carrinhoId);

            // Retorna o carrinho com o Contexto Atual e o Id atribuido ou Obtido

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };

        }

        public void AdicionarCarrinho (Camisa camisa, int quant)
        {
            var carrinhoCompraItem =
                _contexto.CarrinhoCompraDeItens.SingleOrDefault(
                    s => s.Camisa.CamisaId == camisa.CamisaId && s.CarrinhoDeCompraId == CarrinhoCompraId);

                // Verifica se o carinho existe, se não existe cria um carrinho
                if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoDeCompraItem
                {
                    CarrinhoDeCompraId = CarrinhoCompraId,
                    Camisa = camisa,
                    Quantidade = 1
                };
                _contexto.CarrinhoCompraDeItens.Add(carrinhoCompraItem);
            }
            else // Se existir o carrinho com o item então incrementa a quantidade
            {
                carrinhoCompraItem.Quantidade++;
            }
                //Salva as alterações
            _contexto.SaveChanges();    
        }

        public int RemoverDoCarrinho (Camisa camisa)
        {
            // Recebe a camisa via ID e qual carrinho
            var carrinhoCompraItem =
                _contexto.CarrinhoCompraDeItens.SingleOrDefault(
                    r => r.Camisa.CamisaId == camisa.CamisaId && r.CarrinhoDeCompraId == CarrinhoCompraId);
           
            var quantidadeLocal = 0;


            // Verifica se o carrinho é NULO e maior que 1
            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else // Remove o item do Carrinho
                {
                    _contexto.CarrinhoCompraDeItens.Remove(carrinhoCompraItem);
                }
            }

            // Salva as alterações
            _contexto.SaveChanges();
            // Retorna a quantidade de Carrinhos
            return quantidadeLocal;
        }

        public List<CarrinhoDeCompraItem> GetCarrinhoCompraItens()
        {
            // Retorna a lista de Carrinhos
            return CarrinhoDeCompraItems ??
                (CarrinhoDeCompraItems =
                _contexto.CarrinhoCompraDeItens.Where(c => c.CarrinhoDeCompraId == CarrinhoCompraId)
                .Include(s => s.Camisa)
                .ToList());
                
                
        }

        public void LimparCarrinho()
        {
            // Recupera os itens do carrinho a ser excluido
            var carrinhoItens = _contexto.CarrinhoCompraDeItens
                .Where(car => car.CarrinhoDeCompraId == CarrinhoCompraId);

            // Exclui o carrinho selecionado
            _contexto.CarrinhoCompraDeItens.RemoveRange(carrinhoItens);

            //Salva a alteração
            _contexto.SaveChanges();


        }

        public decimal GetValorCarrinhoCompraTotal()
        {
            var total = _contexto.CarrinhoCompraDeItens.Where(c => c.CarrinhoDeCompraId == CarrinhoCompraId)
                .Select(c => c.Camisa.Preco * c.Quantidade).Sum();

            return total;
        }

       }
    }
  
