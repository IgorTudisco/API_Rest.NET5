using FilmesApi.Data;
using FilmesApi.Services;
using FilmesAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            // Add o service de conecção com o DB
            /*
             *  Passando a propriedade UseLazyLoadingProxies
             *  para que o sistema espera o carregamento das
             *  nossas informações.
             *  
             */
            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("FimelConnection")));
            services.AddControllers();

            // Add o AutoMapper
            // Para isso devemos passar alguns parâmetros.
            // Esses parâmetros é para que ele seja usado direto no assembli.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add o Services
            // Injetando a dependência.
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}