using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.ProductVMs
{
    public class AdminProductDetailVM
    {
        [DisplayName("Ürün Id")]
        public Guid Id { get; set; }
        [DisplayName("Barkod")]
        public string Barcode { get; set; }
        [DisplayName("Ürün Adı")]
        public string ProductName { get; set; }
        [DisplayName("Ürün Miktarı")]
        public string Quantity { get; set; }
        [DisplayName("Ürün Birimi")]
        public string Unit { get; set; }
        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Ürün Fotoğrafı")]
        public string? PhotoPath { get; set; }
        [DisplayName("Ürün Markası")]
        public string Title { get; set; }
        [DisplayName("Kategori Id")]
        public Guid CategoryId { get; set; }
        [DisplayName("Ürün Oluşturma Tarihi")]
        public DateTime? CreatedDate { get; set; }
        [DisplayName("Ürün Güncelleme Tarihi")]
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
