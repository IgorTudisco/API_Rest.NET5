using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.DTOs.Gerente;
using FilmesApi.Models;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private GerenteService _gerenteService;
        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto dto)
        {
            ReadGerenteDto readDto = _gerenteService.AdicionaGerente(dto);
            return CreatedAtAction(nameof(ReculperaGerentePorId), new { id = readDto.Id }, readDto);
        }

        [HttpGet]
        public IActionResult ReculperaGerente()
        {
            List<ReadGerenteDto> readDto = _gerenteService.ReculperaGerente();
            if (readDto != null) return Ok(readDto);
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult ReculperaGerentePorId(int id)
        {
            ReadGerenteDto readDto = _gerenteService.ReculperaGerentePorId(id);
            if (readDto != null) return Ok(readDto); 
            return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletaGerente(int id)
        {
            Result resultado = _gerenteService.DeletaGerente(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }

    }
}
