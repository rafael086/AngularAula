using AngularAula.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAula.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDTO>().ForMember(
                dest => dest.Palestrantes, 
                opt => { opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList()); }).ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>().ForMember(
                dest => dest.Eventos, 
                opt => { opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Evento).ToList()); }).ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();

        }
    }
}
