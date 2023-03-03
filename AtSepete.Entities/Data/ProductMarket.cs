using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class ProductMarket:Base
    {
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public virtual Product ProductInMarket { get; set; }
        public virtual Market MarketInProduct { get; set; }
    }
}
