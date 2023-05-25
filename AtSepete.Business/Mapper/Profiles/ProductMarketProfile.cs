using AtSepete.Core.GenericRepository;
using AtSepete.DataAccess.Context;
using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
             //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => GetProductName(src.ProductId)))
             //.ForMember(dest => dest.MarketName, opt => opt.MapFrom(src => GetMarketName(src.MarketId)));
            CreateMap<ProductMarketListDto, ProductMarket>();
            CreateMap<ProductMarket, CreateProductMarketDto>();
            CreateMap<CreateProductMarketDto, ProductMarket>();
            CreateMap<ProductMarket, UpdateProductMarketDto>();
            CreateMap<UpdateProductMarketDto, ProductMarket>();


        }
        //private async Task<string> GetProductName(Guid productId)
        //{
            
        //    using (var dbContext = new EFBaseRepository<Product>()) { }
        //        return _productRepository.GetByDefaultAsync(x => x.Id == productId).Result.GetFullName();
        //}
        //private async Task<string> GetMarketName(Guid marketId)
        //{ 
        //    return _marketRepository.GetByDefaultAsync(x => x.Id == marketId).Result.MarketName;
        //}

    }
}









