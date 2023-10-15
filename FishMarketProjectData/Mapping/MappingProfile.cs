using AutoMapper;
using FishMarketProjectData.Database.Entities;
using FishMarketProjectDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, User>()
            .ReverseMap();
        }
    }
}
