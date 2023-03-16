using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Core.CoreInterfaces
{
    public interface IAsyncOrderableRepository<T>:IAsyncQueryableRepository<T>,IAsyncRepository where T : Base
    {
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> exp);
        Task<IEnumerable<T>> GetDefaultAsync(Expression<Func<T, bool>> exp);
    }
}
