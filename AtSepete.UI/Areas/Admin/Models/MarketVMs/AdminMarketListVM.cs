using AtSepete.Dtos.Dto.Markets;
using AtSepete.UI.ApiResponses.BaseResponse;

namespace AtSepete.UI.Areas.Admin.Models.MarketVMs
{
    public class AdminMarketListVM
    {
        public Guid Id { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
