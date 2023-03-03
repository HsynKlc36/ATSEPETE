using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class OrderDetail:Base
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public virtual Order ProductInOrder { get; set; }
        public virtual Product OrderInProduct { get; set; }
    }
}
