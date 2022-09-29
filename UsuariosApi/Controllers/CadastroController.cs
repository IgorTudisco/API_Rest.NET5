﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroServise;

        public CadastroController(CadastroService cadastroServise)
        {
            _cadastroServise = cadastroServise;
        }

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = _cadastroServise.CadastraUsuario(createDto);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok();
        }
    }
}
