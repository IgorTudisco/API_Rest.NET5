using FilmesApi.Models;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        /*
         * Sobrescrista do método que parametriza a criação do nosso DB.
         * Informando a criação da nossa relação de um para um.
         */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Construa na entidade Enderaço uma relação.
            builder.Entity<Endereco>()
                // A relação do Enderaço tem um Cinema.
                .HasOne(endereco => endereco.Cinema)
                // A entidade Cinema tem uma relação com o Enderaço.
                .WithOne(cinema => cinema.Endereco)
                // A minha chave estranjeira está na entidade Cinema.
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}

/*
 * Migration => add-Migration "nome da migration"; Update-Database
 */