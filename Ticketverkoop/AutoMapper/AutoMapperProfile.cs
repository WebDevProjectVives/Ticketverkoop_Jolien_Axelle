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
        }
    }
}
