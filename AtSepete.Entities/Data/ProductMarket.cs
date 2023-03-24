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
       
        [ForeignKey("ProductInMarket")]
        public Guid ProductId { get; set; }
        [ForeignKey("MarketInProduct")]
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        virtual public Product ProductInMarket { get; set; }
        virtual public Market MarketInProduct { get; set; }
    }
}
