using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Core.CoreInterfaces
{
    public interface IAsyncQueryableRepository<T>:IAsyncRepository where  T : Base
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    }
}
