namespace AtSepete.UI.Areas.Admin.Models.StockVMs
{
    public class AdminUpdateStockVM
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

    }
}
