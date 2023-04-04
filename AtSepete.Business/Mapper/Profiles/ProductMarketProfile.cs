using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Mapper.Profiles
{
    public class ProductMarketProfile : Profile
    {
        public ProductMarketProfile()
        {
            CreateMap<ProductMarket, ProductMarketDto>();
            CreateMap<ProductMarketDto, ProductMarket>();
            CreateMap<ProductMarket, ProductMarketListDto>();
            CreateMap<ProductMarketListDto, ProductMarket>();
            CreateMap<ProductMarket, CreateProductMarketDto>();
            CreateMap<CreateProductMarketDto, ProductMarket>();
            CreateMap<ProductMarket, UpdateProductMarketDto>();
            CreateMap<UpdateProductMarketDto, ProductMarket>();
        }
    }
}
