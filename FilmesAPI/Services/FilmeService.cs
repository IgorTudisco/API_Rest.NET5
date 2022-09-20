using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Services
{
    public class FilmeService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto Adicionafilme(CreateFilmeDto filmeDto)
        {
            // Mapeando um Dto para um Filme
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            // Mapeando um filme para um Dto
            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> ReculperaFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
            }
            else
            {
                // Montando a condição com o Linq e convertendo o resultado para uma lista.
                filmes = _context.Filmes
                    .Where(filmes => filmes.ClassificacaoEtaria <= classificacaoEtaria)
                    .ToList();
            }

            if (filmes != null)
            {

                List<ReadFilmeDto> filmeDtos = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return filmeDtos;
            }

            // Caso a lista seja nula, vamos retornar apenas um null.
            return null;
        }

        public ReadFilmeDto RecuperaFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                return filmeDto;
            }

            return null;

        }

        /*
         * Usando a dependência FluentResult conseguimos usar o Result,
         * para passar resultados mais elaborados.
         * 
         */
        public Result AtualizaFilme(int id, UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _context.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
