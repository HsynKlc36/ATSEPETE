using AtSepete.Dtos.Dto.Markets;
using AtSepete.UI.Areas.Admin.Models.MarketVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class MarketVMProfile:Profile
    {
        public MarketVMProfile()
        {
            CreateMap<AdminMarketListVM,MarketListDto>().ReverseMap();
            CreateMap<AdminMarketCreateVM,CreateMarketDto>().ReverseMap();
            CreateMap<MarketDto,AdminMarketUpdateVM>().ReverseMap();
            CreateMap<MarketDto,AdminMarketDetailVM>().ReverseMap();
            CreateMap<AdminMarketUpdateVM,UpdateMarketDto>().ReverseMap();

        }

    }
}
