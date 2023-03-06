using AtSepete.Entities.BaseData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class Product:Base
    {
        [Key]
        public Guid ProductId { get; set; }
        public string Barcode { get; set; }

        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        public string Title { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        //navigation property
        public Category Category { get; set; }
        public IEnumerable<ProductMarket> ProductMarkets { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }



    }
}
