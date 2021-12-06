using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Profiles
{
    // Extendendo da class Profile
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDTO, Filme>();
            CreateMap<Filme, ReadFilmeDTO>();
            CreateMap<UpdateFilmeDTO, Filme>();
        }
    }
}
