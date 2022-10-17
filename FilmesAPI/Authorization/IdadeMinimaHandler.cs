using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmesApi.Authorization
{
    /*
     * Class que contem a minha lógica de idade mínima.
     * 
     * Para fazer a conexão extendemos de AuthorizationHandler
     * indicando a minha class que contem a minha idade mínima. 
     */
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        // Sobrescrevendo o método de manipulação dá minha política
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinimaRequirement requirement)
        {
            /*
             * Mesmo a Task sendo concluida ela não será autorizada,
             * porque não temos a data do aniversário.
             */
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            /*
             * Convertendo a data em DateTime, pois ela é passada
             * como string no Token
             */
            DateTime dataNascimento = Convert.ToDateTime(context
                .User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth
            ).Value);

            // Calculando a idade da pessoa com base na data de hoje.
            int idadeObtida = DateTime.Today.Year - dataNascimento.Year;

            /*
             *  Porém se ela ainda não fez aniversário,
             *  vamos decrementar um ano.
             *  Fazemos isso comparando as datas. O addYears sempre
             *  soma um valor a data atual, por isso ela recebe o negativo.
             */
            if (dataNascimento > DateTime.Today.AddYears(-idadeObtida))
                idadeObtida--;

            /*
             * Verificação da idade permitida, a mesma foi setada no
             * Startup
             */
            if (idadeObtida >= requirement.IdadeMinima)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
