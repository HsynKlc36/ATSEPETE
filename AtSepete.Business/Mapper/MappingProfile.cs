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
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductListDto>();
            CreateMap<ProductListDto, Product>();
            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>();
            //
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderListDto>();
            CreateMap<OrderListDto, Order>();
            CreateMap<Order, CreateOrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<Order, UpdateOrderDto>();
            CreateMap<UpdateOrderDto, Order>();
            //
            CreateMap<ProductMarket, ProductMarketDto>();
            CreateMap<ProductMarketDto, ProductMarket>();
            CreateMap<ProductMarket, ProductMarketListDto>();
            CreateMap<ProductMarketListDto, ProductMarket>();
            CreateMap<ProductMarket, CreateProductMarketDto>();
            CreateMap<CreateProductMarketDto, ProductMarket>();
            CreateMap<ProductMarket, UpdateProductMarketDto>();
            CreateMap<UpdateProductMarketDto, ProductMarket>();
            //

        }

    }
}
