using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface ICategoryRepository: IAsyncDeleteableRepository<Category>, IAsyncFindableRepository<Category>, IAsyncInsertableRepository<Category>, IAsyncOrderableRepository<Category>, IAsyncQueryableRepository<Category>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<Category>
    {
      
    }
}
