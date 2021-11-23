using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController] // Indicando que essa classe é um controlador.
    [Route("[Controller]")] // Indicando que o meu end point vai ter o mesmo nome da minha class
    public class FilmeController : ControllerBase // Extendendo a class.
    {

        private static List<Filme> filmes = new List<Filme>();

        [HttpPost] // Indicação de um verbo post
        public void AdicionaFilme(Filme filme)
        {

            filmes.Add(filme);
            Console.WriteLine(filme);

        }

    }
}
