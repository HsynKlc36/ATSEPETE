using AtSepete.Entities.Data;
using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.ProductMarketVMs
{
    public class AdminProductMarketListVM
    {
        [DisplayName("Ürün-Market Id")]
        public Guid Id { get; set; }
        [DisplayName("Ürün Id")]
        public Guid ProductId { get; set; }
        [DisplayName("Market Id")]
        public Guid MarketId { get; set; }
        [DisplayName("Market Stok Adedi")]
        public int Stock { get; set; }
        [DisplayName("Market Fiyatı")]
        public decimal Price { get; set; }
        [DisplayName("Market Adı")]
        public string? MarketName { get; set; }
        [DisplayName("Ürün Adı")]
        public string? ProductName { get; set; }

    }
}
