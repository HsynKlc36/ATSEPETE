namespace AtSepete.UI.Areas.Admin.Models.ProductVMs
{
    public class AdminProductListVM
    {
        public Guid Id { get; set; }
        public string Barcode { get; set; }

        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }

        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
    }
}
