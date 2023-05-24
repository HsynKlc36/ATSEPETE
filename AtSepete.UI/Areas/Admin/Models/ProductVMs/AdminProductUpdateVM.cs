using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtSepete.UI.Areas.Admin.Models.ProductVMs
{
    public class AdminProductUpdateVM
    {
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        public string Title { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }//miktar
        public string Unit { get; set; }//birim
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }
        public Guid CategoryId { get; set; }
        public SelectList Categories { get; set; }
    }
}
