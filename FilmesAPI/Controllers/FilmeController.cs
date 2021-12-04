

using FilmesAPI.Data;
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

        private FilmeContext _comtext;

        public FilmeController(FilmeContext filmeContext)
        {
            _comtext = filmeContext;
        }

        // Indicação de um verbo post
        [HttpPost]
        // [FromBody] Indicação que os dados vem no corpo da requidição.
        // IActionResult Tipo dos estados http.
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            // Acessando os dados no DB. Para guardar um dado.
            _comtext.FilmesDB.Add(filme);

            // Salvando as alterações no DB.
            _comtext.SaveChanges();

            // Indicação de retorno do obj. Ele irá retornar o obj criado.
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }

        // Método para trazer os dados.
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return _comtext.FilmesDB;
        }

        // Procurando um filme por Id.
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = _comtext.FilmesDB.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

    }
}