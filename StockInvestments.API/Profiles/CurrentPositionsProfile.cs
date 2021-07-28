using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace StockInvestments.API.Profiles
{
    public class CurrentPositionsProfile : Profile
    {
        public CurrentPositionsProfile()
        {
            CreateMap<Entities.CurrentPosition, Models.CurrentPositionDto>();
            CreateMap<Models.CurrentPositionForCreationDto, Entities.CurrentPosition>();
            CreateMap<Models.CurrentPositionForUpdateDto, Entities.CurrentPosition>();
            CreateMap<Entities.CurrentPosition, Models.CurrentPositionForUpdateDto>();
        }
    }
}
