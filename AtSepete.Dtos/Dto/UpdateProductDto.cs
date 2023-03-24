using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto
{
    public class UpdateProductDto
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
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;// bunlar tüm dto lar da olacak mı yoksa servislerde mi verilmeli sonradan bak!
    }
}
