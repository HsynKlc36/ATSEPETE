using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.Dtos.Dto.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Mapper.Profiles
{
    public class CustomerOrderProfile:Profile
    {
        public CustomerOrderProfile()
        {
            CreateMap<object, CustomerOrderListDto>()
          .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => (Guid)src.GetType().GetProperty("ProductId").GetValue(src, null)))
          .ForMember(dest => dest.MarketId, opt => opt.MapFrom(src => (Guid)src.GetType().GetProperty("MarketId").GetValue(src, null)))
          .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => (string)src.GetType().GetProperty("ProductName").GetValue(src, null)))
          .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => (string)src.GetType().GetProperty("ProductTitle").GetValue(src, null)))
          .ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => (string)src.GetType().GetProperty("ProductQuantity").GetValue(src, null)))
          .ForMember(dest => dest.ProductUnit, opt => opt.MapFrom(src => (string)src.GetType().GetProperty("ProductUnit").GetValue(src, null)))
          .ForMember(dest => dest.ProductPhotoPath, opt => opt.MapFrom(src => (string)src.GetType().GetProperty("ProductPhotoPath").GetValue(src, null)))
          .ForMember(dest => dest.ProductAmount, opt => opt.MapFrom(src => (int)src.GetType().GetProperty("ProductAmount").GetValue(src, null)))
          .ForMember(dest => dest.MarketName, opt => opt.MapFrom(src => (string)src.GetType().GetProperty("MarketName").GetValue(src, null)))
          .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => (decimal)src.GetType().GetProperty("ProductPrice").GetValue(src, null)))
          .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => (Guid)src.GetType().GetProperty("OrderId").GetValue(src, null)))
          .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => (string)src.GetType().GetProperty("CustomerAddress").GetValue(src, null)));
        }
    }
}
