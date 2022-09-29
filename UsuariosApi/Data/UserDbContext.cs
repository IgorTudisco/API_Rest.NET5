using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Data
{
    /*
     * Definindo que essa classe é um IdentityDbContext e definindo que ele terá
     * uma identificação de User do tipo inteiro. Dentro do sistema também
     * passamos uma identificação do tipo inteiro que é gerado pela Role
     * e que ele também vai ter uma chave do tipo inteiro.
     */
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        // Passando algumas opções para o meu construtor base.
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {
        }
    }
}
