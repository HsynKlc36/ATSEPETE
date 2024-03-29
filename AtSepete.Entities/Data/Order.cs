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
    public class Order:Base
    {

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        [ForeignKey("Market")]
        public Guid MarketId { get; set; }
        //navigation property
        virtual public IEnumerable<OrderDetail> OrderDetails { get; set; }
        virtual public User Customer { get; set; }
        virtual public Market Market { get; set; }

    }
}
