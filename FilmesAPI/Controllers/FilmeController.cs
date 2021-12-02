

using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FilmesAPI.Controllers
{
    [ApiController] // Indicando que essa classe é um controlador.
    [Route("[controller]")] // Indicando que o meu end point vai ter o mesmo nome da minha class
    public class FilmeController : ControllerBase // Extendendo a class.
    {

        private static List<Filme> filmes = new List<Filme>();

        // Passando um indentidicador
        public static int id = 1;

        [HttpPost] // Indicação de um verbo post
        // Indicação que os dados vem no corpo da requidição.
        public void AdicionaFilme([FromBody] Filme filme) 
        {
            filme.Id = id++;
            filmes.Add(filme);
            // Console.WriteLine(filme.Titulo);

        }

        // Método para trazer os dados.
        [HttpGet]
        /*
         * Passamos um IEnumerable que pelo polimorfizmo é uma lista.
         * Para possamos passar qual quer classe que implemente o IEnumerable.
        */
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return filmes;
        }

        // Procurando um filme por Id.
        [HttpGet("{id}")] // Passando um id como parâmetro
        public Filme RecuperaFilmePorId(int id)
        {

            foreach(Filme filme in filmes){

                if(filme.Id == id)
                {
                    return filme;
                }

            }

            return null;

        }

    }
}
