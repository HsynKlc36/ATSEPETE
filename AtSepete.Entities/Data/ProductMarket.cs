using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class ProductMarket:Base
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductInMarket")]
        public Guid ProductId { get; set; }
        [ForeignKey("MarketInProduct")]
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public virtual Product ProductInMarket { get; set; }
        public virtual Market MarketInProduct { get; set; }
    }
}
