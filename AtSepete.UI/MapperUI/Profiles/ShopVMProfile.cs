using AtSepete.Dtos.Dto.Shop;
using AtSepete.Entities.Data;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class ShopVMProfile:Profile
    {
        public ShopVMProfile()
        {
            CreateMap<ShopListDto,CustomerShopListVM>().ReverseMap();
            CreateMap<BestSellerProductListDto, CustomerBestSellerListVM>().ReverseMap();
            CreateMap<ShopFilterListDto, CustomerShopFilterListVM>().ReverseMap();
            CreateMap<ShopSideBarFilterListDto, CustomerShopSideBarFilterListVM>().ReverseMap();
            CreateMap<ShopProductDetailDto, CustomerShopProductDetailsVM>().ReverseMap();

        }

    }
    
}
