using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.ViewModel;

namespace Ticketverkoop.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Stadion, StadionVM>();

            CreateMap<Club, ClubVM>().ForMember(dest => dest.Stadion,
                opts => opts.MapFrom(
                    src => src.Stadion.Naam));

            CreateMap<Wedstrijd, WedstrijdVM>().ForMember(dest => dest.Thuisploeg,
                opts => opts.MapFrom(
                    src => src.Thuisploeg.Naam))
            .ForMember(dest => dest.Uitploeg,
                opts => opts.MapFrom(
                    src => src.Uitploeg.Naam))
            .ForMember(dest => dest.Stadion,
                opts => opts.MapFrom(
                    src => src.Stadion.Naam));

            CreateMap<Wedstrijd, CartVM>().ForMember(dest => dest.Thuisploeg,
                opts => opts.MapFrom(
                    src => src.Thuisploeg.Naam))
            .ForMember(dest => dest.Uitploeg,
                opts => opts.MapFrom(
                    src => src.Uitploeg.Naam));

        }
    }
}
