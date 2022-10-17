using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        public CadastroService(IMapper mapper,
            UserManager<IdentityUser<int>> userManager,
            EmailService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);

            // Convertendo para um usuário identity para podemos gravar no db
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            // Com o gerenciador de usuários
            // Cadastrando o user
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            // Adicionando a Role ao usuário
            _userManager.AddToRoleAsync(usuarioIdentity, "regular");

            if (resultadoIdentity.Result.Succeeded)
            {
                // Gerando o código de ativação do e-mail
                var code = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity)
                    .Result;

                /*
                 * Para evitar algum erro de conversão de caracteres, na
                 * hora do envio do código através do nosso Http link, temos
                 * que fazer um encode para ele.
                 */
                var encodedCode = HttpUtility.UrlEncode(code);

                // Enviando um e-mail de confirmação para o usuário
                _emailService.EnviarEmail(new[] { usuarioIdentity.Email },
                    "Link de Ativação", usuarioIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário");
        }

        // Ativação do e-mail
        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            // Procurando o usuário
            var identityUse = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);

            /*
             * Validando o e-mail do usuário passando o identityUse
             * e o código para o método ConfirmEmailAsync.
             */
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUse, request.CodigoDeAtivacao)
                .Result;

            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}
