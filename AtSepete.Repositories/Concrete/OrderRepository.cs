using AtSepete.Core.GenericRepository;
using AtSepete.DataAccess.Context;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Concrete
{
    public class OrderRepository:EFBaseRepository<Order>,IOrderRepository
    {
        public OrderRepository(AtSepeteDbContext Context):base(Context) 
        {

        }
    }
}
