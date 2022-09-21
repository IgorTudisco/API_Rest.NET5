using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Data.DTOs.Gerente
{
    public class UpdateGerenteDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
    }
}
