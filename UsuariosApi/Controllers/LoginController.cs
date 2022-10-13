using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;
using UsuariosApi.Data.Requests;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginServe { get; set; }

        public LoginController(LoginService loginServe)
        {
            _loginServe = loginServe;
        }

        [HttpPost]
        // LoginRequest é análogo a um LoginDto
        public IActionResult LogaUsuario(LoginRequest request)
        {
            Result resultado = _loginServe.LogarUsuario(request);
            /*
             * retornando o resultado com o seus casos de
             * erro e sucesso no caso o token.
             */
            if (resultado.IsFailed) return Unauthorized(resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes.FirstOrDefault());
        }

        // Método recupera senha
        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUser(SolicitaResetRequest request)
        {
            Result resultado = _loginServe.SolicitaResetSenhaUser(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes.FirstOrDefault());
        }

        // Método faz a recuperação da senha
        [HttpPost("/restore-reset")]
        public IActionResult RestorePassword(MakeRestoreRequest request)
        {
            Result resultado = _loginServe.RestorePassword(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes.FirstOrDefault());
        }

    }
}
