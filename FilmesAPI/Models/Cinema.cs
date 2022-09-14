using FilmesApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }

        // Relacionamento 1x1
        /*
         *  Um modificador de acesso virtual indica que essa será
         *  uma propriedade de carregamento lazy
         *  
         */
        public virtual Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }

        // Estabelecendo relação de 1:n
        public virtual Gerente Gerente { get; set; }
        public int GerenteId { get; set; }

    }
}