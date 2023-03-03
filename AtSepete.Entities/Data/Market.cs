using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class Market:Base
    {
        [Key]
        public Guid MarketId { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        //navigation property
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
