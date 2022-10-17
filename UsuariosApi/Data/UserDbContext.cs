using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Models;

namespace UsuariosApi.Data
{
    /*
     * Definindo que essa classe é um IdentityDbContext e definindo que ele terá
     * uma identificação de User do tipo inteiro. Dentro do sistema também
     * passamos uma identificação do tipo inteiro que é gerado pela Role
     * e que ele também vai ter uma chave do tipo inteiro.
     */
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {
        // Usando o configuration para usar o arquivo Secrets
        private IConfiguration _configuration;

        // Passando algumas opções para o meu construtor base.
        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }

        /*
         * Sobrescrevendo o método created, para criarmos um usuário
         * admin e ele terá as roles de adm
         */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Criando usuário fora do fluxo padrão
            CustomIdentityUser admin = new CustomIdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                // Passando um identificadar único para o nosso adm
                SecurityStamp = Guid.NewGuid().ToString(),
                // Passando um id único
                Id = 99999
            };

            // Gerando a senha pelo hash
            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();

            // Usando o hasher ele vai criptografar nossa senha
            // O GetValue vai trazer a nossa senha do Secrets
            admin.PasswordHash = hasher.HashPassword(admin,
                _configuration.GetValue<string>("admininfo:password"));

            // Para salvar no DB usamos o HasData

            // Criando a entidade adm
            builder.Entity<CustomIdentityUser>().HasData(admin);

            // Criando a role de admin
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 99999,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                }
            );

            // Criando a role regular
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 99998,
                    Name = "regular",
                    NormalizedName = "REGULAR"
                }
            );

            // Fazendo o binding entre o usuario e a role
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 }
            );
        }

        /*
         * Agora temos que add uma migration para ele biuldar o nosso
         * usuário admin e um update
         */
    }
}
