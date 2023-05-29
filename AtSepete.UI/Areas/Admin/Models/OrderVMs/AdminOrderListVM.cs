namespace AtSepete.UI.Areas.Admin.Models.OrderVMs
{
    public class AdminOrderListVM
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid MarketId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
