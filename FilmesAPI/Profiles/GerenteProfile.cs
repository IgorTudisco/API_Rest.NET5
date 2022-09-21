using AutoMapper;
using FilmesApi.Data.DTOs.Gerente;
using FilmesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Profiles
{
    public class GerenteProfile : Profile
    {

        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            /*
             *  Para evitar o problema de redundancia,
             *  criaremos um obj usando um obj anonimo da nossa
             *  classe ReadGerenteDto, passando somente as infos
             *  necessárias.
             */
            CreateMap<Gerente, ReadGerenteDto>()
                /*
                 *  Quando for mapear de Gerente para ReadGerenteDto,
                 *  Pega o membro Cinemas da classe ReadGerenteDto e
                 *  mapea ele usando a lista Cinemas da classe Gerente,
                 *  Mas só com os dados selecionadas.
                 */
                .ForMember(gerente => gerente.Cinemas, opts => opts
                .MapFrom(gerente => gerente.Cinemas.Select
                (c => new { c.Id, c.Nome, c.Endereco, c.EnderecoId })));
            CreateMap<UpdateGerenteDto, Gerente>();
        }

    }
}
