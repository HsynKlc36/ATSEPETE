using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface IProductMarketRepository : IAsyncDeleteableRepository<ProductMarket>, IAsyncFindableRepository<ProductMarket>, IAsyncInsertableRepository<ProductMarket>, IAsyncOrderableRepository<ProductMarket>, IAsyncQueryableRepository<ProductMarket>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<ProductMarket>
    {
        Task<IQueryable<ProductMarket>> GetAllQueryableAsync();
    }
}
