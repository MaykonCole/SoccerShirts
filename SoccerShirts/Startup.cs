using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoccerShirts.Context;
using SoccerShirts.Models;
using SoccerShirts.Repositories;

namespace SoccerShirts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Registro de Dependencia APPDBCONTEXT, passando a minha string de Conexão que está na APPSETINGS
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Adicionar recurso IDENTITY para autenticação de usuários
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
                

            // Injecao de DEPENDÊNCIA das INTERFACES
            //Objeto é criado toda vez que for requisitado
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ICamisaRepository, CamisaRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            //Objeto é criado o mesmo para todas requisições
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // É criado um objeto Carrinho para cada requisição
            services.AddScoped(cp => CarrinhoCompra.GetCarrinho(cp));

            // Adiciona a Dependencia MVC
            services.AddControllersWithViews();

            // Adiciona o cache de Mémoria
            services.AddMemoryCache();

            // Adiciona o recurso da sessão
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            // Acessar arquivos estáticos
            app.UseStaticFiles();
            // Acessar a sessão no contexto
            app.UseSession();

            // Realizar autenticação de usuário
            app.UseAuthentication();

            // Uso de rotas
            app.UseRouting();
            // Uso de autorização não autenticada
            app.UseAuthorization();

            // Uso dos recursos MVC
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "AdminArea",
                    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(name: "categoriaFiltro",
                 pattern: "Camisa/{action}/{categoria?}",
                    defaults: new { Controller = "Camisa", action = "ListarCamisas" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
