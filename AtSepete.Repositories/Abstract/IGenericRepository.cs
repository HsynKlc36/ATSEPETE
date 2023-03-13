using AtSepete.Entities.BaseData;
using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : Base
    {
       Task <T> GetByIdAsync(Guid id);
       Task <IEnumerable<T>> GetAllAsync();
       Task <bool> AddAsync(T item);       
       Task <bool> RemoveAsync(T item);
       Task <bool> UpdateAsync(T item);
       Task <bool> UpdateAsync(IEnumerable<T> items);
       Task <bool> Save();
       Task<IEnumerable<T>> Where(Expression<Func<T, bool>> exp);
       Task <T> GetByDefaultAsync(Expression<Func<T, bool>> exp);
       Task <IEnumerable<T>> GetDefaultAsync(Expression<Func<T, bool>> exp);
    
       Task<bool> SetPassiveAsync(Expression<Func<T, bool>> exp);

    }
}
