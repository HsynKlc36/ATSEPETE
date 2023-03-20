using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface IOrderRepository : IAsyncDeleteableRepository<Order>, IAsyncFindableRepository<Order>, IAsyncInsertableRepository<Order>, IAsyncOrderableRepository<Order>, IAsyncQueryableRepository<Order>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<Order>
    {
    }
}
