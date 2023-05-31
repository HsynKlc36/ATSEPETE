namespace AtSepete.UI.Areas.Admin.Models.StockVMs
{
    public class AdminStockListVM
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? MarketName { get; set; }
        public string? ProductName { get; set; }
    }
}
