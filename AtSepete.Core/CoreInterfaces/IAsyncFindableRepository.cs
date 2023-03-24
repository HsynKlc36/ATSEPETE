using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Core.CoreInterfaces
{
    public interface IAsyncFindableRepository<T>:IAsyncQueryableRepository<T>,IAsyncRepository where T : Base
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByIdActiveOrPassiveAsync(Guid id);

        Task<T?> GetByDefaultAsync(Expression<Func<T, bool>> exp);
  
        Task<bool> AnyAsync(Expression<Func<T, bool>>? expression = null);
    }
}
