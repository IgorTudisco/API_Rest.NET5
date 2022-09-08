using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FilmesApi.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }

        // Relacionamento 1-1
        /*
         *  Um modificador de acesso virtual indica que essa será
         *  uma propriedade de carregamento lazy.
         *  
         *  Para resolver o problema do ciclo infinito
         *  de referências usamos o JsonIgnore.
         *  
         */
        [JsonIgnore]
        public virtual Cinema Cinema { get; set; }


    }
}
