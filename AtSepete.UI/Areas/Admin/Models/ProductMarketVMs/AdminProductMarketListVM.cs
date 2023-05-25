using AtSepete.Entities.Data;

namespace AtSepete.UI.Areas.Admin.Models.ProductMarketVMs
{
    public class AdminProductMarketListVM
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Market? Market { get; set; }
        public Product? Product { get; set; }
    }
}
