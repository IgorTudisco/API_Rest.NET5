using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            // Convertendo para um usuário identity para podemos gravar no db
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            // Com o gerenciador de usuários
            // Cadastrando o user
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            if (resultadoIdentity.Result.Succeeded)
            {
                // Gerando o código de ativação do e-mail
                var code = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity)
                    .Result;
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
