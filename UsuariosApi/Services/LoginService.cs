using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _singInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> singInManager, TokenService tokenService)
        {
            _singInManager = singInManager;
            _tokenService = tokenService;
        }

        public Result LogarUsuario(LoginRequest request)
        {
            // Fazendo a autenticação de login
            var resultadoIdentity = _singInManager
                /*
                 * Passamos como false alguns parâmetros de login,
                 * como travas e outas coisas.
                 */
                .PasswordSignInAsync(request.Username, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                // Pegando o IdentityUser do usuário
                var identityUser = _singInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario =>
                    usuario.NormalizedUserName == request.Username.ToUpper()
                    );
                // Gerando o Token
                Token token = _tokenService.CreateToken(identityUser);

                /*
                 * Passando o meu token para o usuário, dizendo
                 * que ele teve um sucesso relacionado ao token.
                 */
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }
    }
}
