﻿using AtSepete.Entities.BaseData;
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

        [ForeignKey("ProductInOrder")]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderInProduct")]
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        virtual public  Order ProductInOrder { get; set; }
        virtual public  Product OrderInProduct { get; set; }
    }
}
