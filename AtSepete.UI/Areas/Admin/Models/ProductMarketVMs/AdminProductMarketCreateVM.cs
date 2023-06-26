using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.ProductMarketVMs
{
    public class AdminProductMarketCreateVM
    {
        [DisplayName("Ürün Adı")]
        public Guid ProductId { get; set; }
        [DisplayName("Market Adı")]
        public Guid MarketId { get; set; }
        [DisplayName("Market Stok Adedi")]
        public int Stock { get; set; }
        [DisplayName("Market Ürün Fiyatı")]
        public decimal Price { get; set; }
        public SelectList Products { get; set; }
        public SelectList Markets { get; set; }
    }
}
