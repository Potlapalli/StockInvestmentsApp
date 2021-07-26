using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace StockInvestments.API.Profiles
{
    public class StockEarningsProfile :  Profile
    {
        public StockEarningsProfile()
        {
            CreateMap<Entities.StockEarning, Models.StockEarningDto>();
            CreateMap<Models.StockEarningForCreationDto, Entities.StockEarning>();
            CreateMap<Models.StockEarningForManipulationDto, Entities.StockEarning>();
        }
    }
}
