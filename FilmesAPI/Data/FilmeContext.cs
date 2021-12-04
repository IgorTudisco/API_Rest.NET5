using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
/*
* Foi necessário colocar essas extenções para conectar ao DB.
* EntityFrameworkCore.Tools
* EntityFrameworkCore
* MySQL.EntityFrameworkCore
*/
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    // A class tem que herdar de DbContext para fazer a ligação com o DB.
    public class FilmeContext : DbContext
    {
        // ctor mais 2xTab para criar o construtor pelo atalho.
        // Devemos passar o contexto do DB como parâmetro e indicar o seu type.
        /*
         * Então as nossas opções de contexto são relacionadas a class fime.
         * Devemos passar esse parâmetro para o nosso DB context.
         */
        public FilmeContext(DbContextOptions<FilmeContext> opt) : base(opt)
        {

        }

        // Atributo que irá fazer o meu acesso ao meu DB.
        public DbSet<Filme> FilmesDB { get; set; }

    }
}
