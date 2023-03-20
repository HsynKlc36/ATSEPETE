using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface IUserRepository : IAsyncDeleteableRepository<User>, IAsyncFindableRepository<User>, IAsyncInsertableRepository<User>, IAsyncOrderableRepository<User>, IAsyncQueryableRepository<User>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<User>
    {

    }
}
