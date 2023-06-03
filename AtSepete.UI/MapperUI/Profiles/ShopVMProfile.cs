using AtSepete.Dtos.Dto.Shop;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class ShopVMProfile:Profile
    {
        public ShopVMProfile()
        {
            CreateMap<ShopListDto,CustomerShopListVM>().ReverseMap();
        }
    }
}
