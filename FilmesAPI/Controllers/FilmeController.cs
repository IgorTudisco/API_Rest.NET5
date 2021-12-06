

using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController] // Indicando que essa classe é um controlador.
    [Route("[controller]")] // Indicando que o meu end point vai ter o mesmo nome da minha class
    public class FilmeController : ControllerBase // Extendendo a class.
    {
        // Usando a propriedade contexto, para acessar o DB.

        private FilmeContext _context;

        public FilmeController(FilmeContext filmeContext)
        {
            _context = filmeContext;
        }

        // Indicação de um verbo post
        [HttpPost]
        // [FromBody] Indicação que os dados vem no corpo da requidição.
        // IActionResult Tipo dos estados http.
        // Passando uma DTO e fazendo a sua converção.
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO)
        {
            // Fazendo a converção.
            Filme filme = new Filme
            {
                Titulo = filmeDTO.Titulo,
                Genero = filmeDTO.Genero,
                Duracao = filmeDTO.Duracao,
                Diretor = filmeDTO.Diretor
            };

            // Acessando os dados no DB. Para guardar um dado.
            _context.FilmesDB.Add(filme);

            // Salvando as alterações no DB.
            _context.SaveChanges();

            // Indicação de retorno do obj. Ele irá retornar o obj criado.
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }

        // Método para trazer os dados.
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return _context.FilmesDB;
        }

        // Procurando um filme por Id.
        // Colocando uma DTO.
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = _context.FilmesDB.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                // Passando o filme para a DTO
                ReadFilmeDTO filmeDTO = new ReadFilmeDTO
                {
                    Id = filme.Id,
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    Genero = filme.Genero,
                    HoraDaConsulta = DateTime.Now
                };

                return Ok(filmeDTO);
            }
            return NotFound();
        }

        // Método de atualização.
        // Passando um DTO
        [HttpPost("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
        {
            // Verificando se existe o filme no DB.
            Filme filme = _context.FilmesDB.FirstOrDefault(filme => filme.Id == id);
            
            // Verificação simples se o filme está nulo.
            if (filme == null)
            {
                return BadRequest();
            }

            filme.Titulo = filmeDTO.Titulo;
            filme.Genero = filmeDTO.Genero;
            filme.Duracao = filmeDTO.Duracao;
            filme.Diretor = filmeDTO.Diretor;
            _context.SaveChanges();

            // A boa pratica no Put é não retornar nada.
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            // Verificando se existe o filme no DB.
            Filme filme = _context.FilmesDB.FirstOrDefault(filme => filme.Id == id);

            // Verificação simples se o filme está nulo.
            if (filme == null)
            {
                return BadRequest();
            }

            // Removendo um item no DB.
            _context.Remove(filme);

            // Salvando as alterações.
            _context.SaveChanges();

            // A boa pratica diz que não devemos retornar nada.
            return NoContent();

        }

    }
}