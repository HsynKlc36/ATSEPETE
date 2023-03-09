﻿using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Concrete
{
    public class OrderDetailRepository:GenericRepository<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailRepository(AtSepeteDbContext Context):base(Context) 
        {

        }
    }
}