using AtSepete.Dtos.Dto.Markets;
using AtSepete.UI.ApiResponses.BaseResponse;
using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.MarketVMs
{
    public class AdminMarketListVM
    {
        [DisplayName("Market Id")]
        public Guid Id { get; set; }
        [DisplayName("Market Adı")]
        public string MarketName { get; set; }
        [DisplayName("Market Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Market Adresi")]
        public string Adress { get; set; }
        [DisplayName("Market Numarası")]
        public string PhoneNumber { get; set; }
    }
}
