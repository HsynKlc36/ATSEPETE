using AtSepete.Dtos.Dto.Markets;
using AtSepete.UI.Areas.Admin.Models.MarketVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class MarketVMProfile:Profile
    {
        public MarketVMProfile()
        {
            CreateMap<AdminMarketListVM,MarketListDto>().ReverseMap();

        }

    }
}
