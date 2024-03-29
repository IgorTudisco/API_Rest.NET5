using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi
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
            /*
             * Definindo que ser� usado um banco de dados e um contexto.
             * Usando
             */
            services.AddDbContext<UserDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("UsuarioConnection"))
            );
            services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
                // Pedindo a confima��o do e-mail
                opt => opt.SignIn.RequireConfirmedEmail = true
                )
                // Usamos uma Stores para armazenar os nossos dados
                .AddEntityFrameworkStores<UserDbContext>()
                // Indicando que virar um c�digo de ativa��o
                .AddDefaultTokenProviders();
            services.AddScoped<EmailService, EmailService>();
            services.AddScoped<CadastroService, CadastroService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<LogoutService, LogoutService>();
            services.AddControllers();
            // Passando a configura��o do AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

/*
 *  Documenta��o do Identity
 *  https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-5.0#password
 *  Outra poss�vel configura��o para o Identity:
 *  services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
    });
 */
