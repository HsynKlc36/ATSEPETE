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
    public class OrderDetail:Base
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductInOrder")]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderInProduct")]
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public virtual Order ProductInOrder { get; set; }
        public virtual Product OrderInProduct { get; set; }
    }
}
