using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace StockInvestments.API.Profiles
{
    public class ClosedPositionsProfile : Profile
    {
        public ClosedPositionsProfile()
        {
            CreateMap<Entities.ClosedPosition, Models.ClosedPositionDto>();
            CreateMap<Models.ClosedPositionForCreationDto, Entities.ClosedPosition>();
            CreateMap<Models.ClosedPositionForManipulationDto, Entities.ClosedPosition>();
        }
    }
}
