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
         * Informando a criação das nossas relações.
         */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Construa na entidade Enderaço uma relação. (1:1)
            builder.Entity<Endereco>()
                // A relação do Enderaço tem um Cinema.
                .HasOne(endereco => endereco.Cinema)
                // A entidade Cinema tem uma relação com o Enderaço.
                .WithOne(cinema => cinema.Endereco)
                // A minha chave estranjeira está na entidade Cinema.
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);

            // Relação de Cinema e Gerente. (1:n)
            builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas)
                /*
                 * Não fazermos a declaração explicita nesse caso,
                 * porque começamos com um cinema e terminamos com
                 * um cinema.
                 */
                .HasForeignKey(cinema => cinema.GerenteId);

            /*
             * Do jeito que está a nossa Migration foi criada
             * com a deleção em cascata, assim se eu deletar um
             * obj todos aqueles que depende dele também serão deletados.
             * Na Migration a ForeignKey vai conter o tipo de deleção
             * ".OnDelete(DeleteBehavior.Cascade)".
             * 
             * Para mudar esse comportamento temos dois jeitos.
             * 
             * Na hora do Bilder basta setar o OnDelete como restrito
             * assim ele vai restringir a minha deleção.
                "builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas)
                .HasForeignKey(cinema => cinema.GerenteId)
                .OnDelete(DeleteBehavior.Restrict);"                    
             * 
             * Podemos ainda informar que o obj dependente poderá ser
             * criado com uma chave nula usando a Required como false.
                "builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas)
                .HasForeignKey(cinema => cinema.GerenteId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);"                    
             * 
             * Depois é só gerar uma nova migration e atualizar o DB.
             * 
             */

            // Criando a ligação n:n na tabela Sessao.
            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Filme)
                .WithMany(filme => filme.Sessoes)
                .HasForeignKey(sessao => sessao.FilmeId);

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessoes)
                .HasForeignKey(sessao => sessao.CinemaId);

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

    }
}

/*
 * Migration => add-Migration "nome da migration"; Update-Database
 */