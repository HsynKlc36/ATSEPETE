using AtSepete.Dtos.Dto.Shop;
using AtSepete.Dtos.Dto.Stocks;
using AtSepete.UI.Areas.Admin.Models.StockVMs;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class StockVMProfile:Profile
    {
        public StockVMProfile()
        {
            CreateMap<StockListDto, AdminStockListVM>().ReverseMap();
            CreateMap<StockDto, AdminUpdateStockVM>().ReverseMap();
            CreateMap<AdminUpdateStockVM, UpdateStockDto>().ReverseMap();

        }
    }
}
