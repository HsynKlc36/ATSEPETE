using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface IMarketRepository : IAsyncDeleteableRepository<Market>, IAsyncFindableRepository<Market>, IAsyncInsertableRepository<Market>, IAsyncOrderableRepository<Market>, IAsyncQueryableRepository<Market>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<Market>
    {
    }
}
