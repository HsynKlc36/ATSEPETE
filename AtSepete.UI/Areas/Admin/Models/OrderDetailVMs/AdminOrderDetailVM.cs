namespace AtSepete.UI.Areas.Admin.Models.OrderDetailVMs
{
    public class AdminOrderDetailVM
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ProductName { get; set; }
        public string? MarketName { get; set; }
        public string? CustomerName { get; set; }
    }
}
