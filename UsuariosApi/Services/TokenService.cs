using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    // Class responsável pela criação do Token
    public class TokenService
    {
        // Criando nosso Token Jwt
        public Token CreateToken(IdentityUser<int> user, string role)
        {
            // Identificando o usuário
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString()), // Espera uma estring
                new Claim(ClaimTypes.Role, role) // Adicionando a role no token
            };

            // Regra de armazenagem dos bytes
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0iatamd090ksdiyg090bhgf090kjloit090wadfrs")
                );

            // Código de Autenticação de Mensagem baseado em Hash
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            // Passando as informações para dentro do token.
            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais,
                // Após 1h o token não vai ser mais válido
                expires: DateTime.UtcNow.AddHours(1)
                );

            // Transformando nosso token para uma string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}
