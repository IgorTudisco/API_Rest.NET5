

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

        private static List<Filme> filmes = new List<Filme>();

        // Passando um indentidicador
        private static int id = 1;

        // Indicação de um verbo post
        [HttpPost]
        // [FromBody] Indicação que os dados vem no corpo da requidição.
        // IActionResult Tipo dos estados http.
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }
        // Método para trazer os dados.
        [HttpGet]
        /*
         * Passamos um IEnumerable que pelo polimorfizmo é uma lista.
         * Para possamos passar qual quer classe que implemente o IEnumerable.
        */
        public IActionResult RecuperaFilmes()
        {
            return Ok(filmes);
        }

        // Procurando um filme por Id.
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

    }
}