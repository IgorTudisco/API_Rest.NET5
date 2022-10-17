using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        // Indica que esse meu tipo vai ser usado como uma senha.
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        // Indica que esse atributo será comparado com um outro.
        [Compare("Password")]
        public string Repassword { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
