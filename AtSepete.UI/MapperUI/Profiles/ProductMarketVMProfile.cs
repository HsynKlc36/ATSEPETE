using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.UI.Areas.Admin.Models.ProductMarketVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class ProductMarketVMProfile:Profile
    {
        public ProductMarketVMProfile()
        {
            CreateMap<AdminProductMarketCreateVM, CreateProductMarketDto>().ReverseMap();
            CreateMap<AdminProductMarketListVM, ProductMarketListDto>().ReverseMap();
            CreateMap<ProductMarketDto, AdminProductMarketUpdateVM>().ReverseMap();
            CreateMap<AdminProductMarketUpdateVM, UpdateProductMarketDto>().ReverseMap();
            




        }
    }
}
