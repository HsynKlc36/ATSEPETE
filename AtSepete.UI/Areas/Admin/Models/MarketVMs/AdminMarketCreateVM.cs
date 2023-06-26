using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AtSepete.UI.Areas.Admin.Models.MarketVMs
{
    public class AdminMarketCreateVM
    {
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
