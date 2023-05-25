using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtSepete.UI.Areas.Admin.Models.ProductMarketVMs
{
    public class AdminProductMarketCreateVM
    {
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public SelectList Products { get; set; }
        public SelectList Markets { get; set; }
    }
}
