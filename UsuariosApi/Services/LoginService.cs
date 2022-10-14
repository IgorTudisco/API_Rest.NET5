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
                // Passando a Role do usuário
                Token token = _tokenService
                    .CreateToken(identityUser, _singInManager
                                 .UserManager.GetRolesAsync(identityUser)
                                 .Result.FirstOrDefault());

                /*
                 * Passando o meu token para o usuário, dizendo
                 * que ele teve um sucesso relacionado ao token.
                 */
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }

        public Result SolicitaResetSenhaUser(SolicitaResetRequest request)
        {
            // Pesquisando o usuário pelo email
            IdentityUser<int> identityUser = RecuperaUsuarioPoremail(request.Email);

            if (identityUser != null)
            {
                // Montando o nosso codigo
                string codigoDeRecuperacao = _singInManager
                    .UserManager
                    .GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");

        }


        internal Result RestorePassword(MakeRestoreRequest request)
        {
            IdentityUser<int> identityUser = RecuperaUsuarioPoremail(request.Email);

            IdentityResult identityResult = _singInManager
                .UserManager
                .ResetPasswordAsync(
                identityUser, request.Token, request.Password)
                .Result;
            if (identityResult.Succeeded)
                return Result
                    .Ok().WithSuccess("Senha redefinida com sucesso");
            return Result.Fail("Houve um erro na operação");
        }

        // Método comumente usado pelos outros métodos
        private IdentityUser<int> RecuperaUsuarioPoremail(string email)
        {
            return _singInManager
                            .UserManager
                            .Users
                            .FirstOrDefault(u =>
                            u.NormalizedEmail == email.ToUpper());
        }
    }
}


/*
 * Para bloquear tentativas de acesso ao sistema, como ataques de força bruta,
 * o identity provê uma alternativa para bloquear tentativas suspeitas de
 * login durante a execução do método de autenticação.
 * 
 *  https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-5.0
 */