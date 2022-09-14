using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Data.DTOs.Gerente
{
    public class ReadGerenteDto
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        /*
         * Criamos um obj anônimo para podemos
         * construir ele da forma que queremos.
         */
        public object Cinemas { get; set; }

    }
}
