using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Concrete
{
    public class OrderRepository:GenericRepository<Order>,IOrderRepository
    {
        public OrderRepository(AtSepeteDbContext Context):base(Context) 
        {

        }
    }
}
