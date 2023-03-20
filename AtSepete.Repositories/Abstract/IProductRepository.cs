using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface IProductRepository : IAsyncDeleteableRepository<Product>, IAsyncFindableRepository<Product>, IAsyncInsertableRepository<Product>, IAsyncOrderableRepository<Product>, IAsyncQueryableRepository<Product>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<Product>
    {
    }
}
