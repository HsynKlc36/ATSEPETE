using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.OrderDetailVMs
{
    public class AdminOrderDetailListVM
    {
        [DisplayName("Sipariş Detay Id")]
        public Guid Id { get; set; }
        [DisplayName("Sipariş Id")]
        public Guid OrderId { get; set; }
        [DisplayName("Ürün Id")]
        public Guid ProductId { get; set; }
        [DisplayName("Sipariş Ürün Adedi")]
        public int Amount { get; set; }
        [DisplayName("Sipariş Oluşturma Tarihi")]
        public DateTime? CreatedDate { get; set; }
        [DisplayName("Sipariş Güncelleme Tarihi")]
        public DateTime? ModifiedDate { get; set; }
        [DisplayName("Ürün Adı")]
        public string? ProductName { get; set; }
        [DisplayName("Müşteri Adı")]
        public string? CustomerName { get; set; }

    }
}
