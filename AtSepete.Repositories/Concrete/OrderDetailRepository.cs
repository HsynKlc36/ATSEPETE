using AtSepete.Core.GenericRepository;
using AtSepete.DataAccess.Context;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Concrete
{
    public class OrderDetailRepository:EFBaseRepository<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailRepository(AtSepeteDbContext Context):base(Context) 
        {

        }
        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailAsync()
        {
            return await GetAllActives().GroupBy(x=>x.OrderId).SelectMany(x=>x.OrderByDescending(x=>x.CreatedDate).AsQueryable()).ToListAsync();
        }
    }
}
