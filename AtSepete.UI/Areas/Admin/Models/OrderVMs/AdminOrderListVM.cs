using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.OrderVMs
{
    public class AdminOrderListVM
    {
        [DisplayName("Sipariş Id")]
        public Guid Id { get; set; }
        [DisplayName("Müşteri Id")]
        public Guid CustomerId { get; set; }
        [DisplayName("Market Id")]
        public Guid MarketId { get; set; }
        [DisplayName("Sipariş Tarihi")]
        public DateTime CreatedDate { get; set; }
    }
}
