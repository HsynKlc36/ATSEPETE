using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();
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
