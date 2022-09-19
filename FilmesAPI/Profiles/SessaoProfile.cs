using AutoMapper;
using FilmesApi.Data.DTOs.Sessao;
using FilmesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                // Calculando o horário de início em tempo de execução.
                // Para esse membro mapei ele da seguinte forma.
                .ForMember(dto => dto.HorarioDeInicio, opts => opts
                /*
                 *  Pegamos o horário de início e somamos a duração a ele,
                 *  porém como na verdade queremos subtrair os minutos, então
                 *  mutiplicamos a duração por -1.
                 */
                .MapFrom(dto =>
                dto.HorarioDeEncerramento.AddMinutes(dto.Filme.Duracao * (-1))));
        }
    }
}
