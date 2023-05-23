using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Products
{
    public class CreateProductDto
    {
        public string Barcode { get; set; }

        public string Title { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }//miktar
        public string Unit { get; set; }//birim
        public string Description { get; set; }
        public string? PhotoFileName { get; set; }
        public string? PhotoPath { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;// bunlar tüm dto lar da olacak mı yoksa servislerde mi verilmeli sonradan bak!
    }
}
