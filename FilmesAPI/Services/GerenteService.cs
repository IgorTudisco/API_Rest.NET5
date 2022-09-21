using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.DTOs.Gerente;
using FilmesApi.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Services
{
    public class GerenteService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionaGerente(CreateGerenteDto dto)
        {
            Gerente gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public ReadGerenteDto ReculperaGerentePorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente != null)
            {

                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
                return gerenteDto;

            }
            return null;
        }

        internal List<ReadGerenteDto> ReculperaGerente()
        {
            List<Gerente> gerente = _context.Gerentes.ToList();
            if (gerente == null) return null;
            return _mapper.Map<List<ReadGerenteDto>>(gerente);
        }

        internal Result AtualizaGerente(int id, UpdateGerenteDto gerenteDto)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _mapper.Map(gerenteDto, gerente);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }
            _context.Remove(gerente);
            _context.SaveChanges();
            return Result.Ok();
        }

    }
}
