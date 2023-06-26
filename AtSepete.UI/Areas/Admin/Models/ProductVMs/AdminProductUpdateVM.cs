using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.ProductVMs
{
    public class AdminProductUpdateVM
    {
        [DisplayName("Ürün Id")]
        public Guid Id { get; set; }
        [DisplayName("Barkod")]
        public string Barcode { get; set; }
        [DisplayName("Ürün Markası")]
        public string Title { get; set; }
        [DisplayName("Ürün Adı")]
        public string ProductName { get; set; }
        [DisplayName("Ürün Miktarı")]
        public string Quantity { get; set; }
        [DisplayName("Ürün Birimi")]
        public string Unit { get; set; }
        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Fotoğraf")]
        public IFormFile? Photo { get; set; }
        [DisplayName("Fotoğraf Yolu")]
        public string? PhotoPath { get; set; }
        [DisplayName("Kategori Adı")]
        public Guid CategoryId { get; set; }
        public SelectList Categories { get; set; }
    }
}
