using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface IOrderDetailRepository : IAsyncDeleteableRepository<OrderDetail>, IAsyncFindableRepository<OrderDetail>, IAsyncInsertableRepository<OrderDetail>, IAsyncOrderableRepository<OrderDetail>, IAsyncQueryableRepository<OrderDetail>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<OrderDetail>
    {
    }
}
