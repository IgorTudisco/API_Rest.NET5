using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Models
{
    public class Token
    {
        // Como vamos apenas passar uma mensagem, não precisamos do set
        public string Value { get; }

        public Token(string value)
        {
            Value = value;
        }
    }
}
