using FilmesApi.Data;
using FilmesApi.Services;
using FilmesAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FilmesAPI
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
            // Add o service de conec��o com o DB
            /*
             *  Passando a propriedade UseLazyLoadingProxies
             *  para que o sistema espera o carregamento das
             *  nossas informa��es.
             *  
             */
            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("FimelConnection")));

            // Adicionando a autentica��o via token
            services.AddAuthentication(auth =>
            {
                // Indicando que a autentica��o ter� um Jwt
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Provando que o Jwt � verdadeiro
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Algumas defini��es da nossa autentica��o
            .AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                // Armazenando o token
                token.SaveToken = true;

                // Os par�metros que seram validado pelo sistema
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    /*
                     * O valor deve ser o mesmo que foi definido no
                     * TokenService do UsuariosApi (onde o token foi
                     * gerado)
                     * 
                     */
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("0iatamd090ksdiyg090bhgf090kjloit090wadfrs")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    /*
                     * Como o nosso Token tem validade de uma hora,
                     * ele ao autenticar o token, vai contar apartir do
                     * zero o tempo de uma hora.
                     * 
                     */
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();

            // Add o AutoMapper
            // Para isso devemos passar alguns par�metros.
            // Esses par�metros � para que ele seja usado direto no assembli.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add o Services
            // Injetando a depend�ncia.
            services.AddScoped<CinemaService, CinemaService>();
            services.AddScoped<EnderecoService, EnderecoService>();
            services.AddScoped<FilmeService, FilmeService>();
            services.AddScoped<GerenteService, GerenteService>();
            services.AddScoped<SessaoService, SessaoService>();

        }

        //ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

        //        return sessaoDto;
        //    }
        //    return null;
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Autenticando o meu token
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}