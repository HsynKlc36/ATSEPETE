using AtSepete.Dtos.Dto.Markets;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Mapper.Profiles
{
    public class MarketProfile:Profile
    {
        public MarketProfile()
        {
            CreateMap<Market, MarketDto>();
            CreateMap<MarketDto, Market>();
            CreateMap<Market, MarketListDto>();
            CreateMap<MarketListDto, Market>();
            CreateMap<Market, CreateMarketDto>();
            CreateMap<CreateMarketDto, Market>();
            CreateMap<Market, UpdateMarketDto>();
            CreateMap<UpdateMarketDto, Market>();
        }
    }
}
