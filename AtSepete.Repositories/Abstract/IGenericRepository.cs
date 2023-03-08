using AtSepete.Entities.BaseData;
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
       Task <T> GetByDefaultAsync(Expression<Func<T, bool>> exp);
       Task <IEnumerable<T>> GetDefaultAsync(Expression<Func<T, bool>> exp);
       Task <IEnumerable<T>> GetAllAsync();
       Task <IEnumerable<T>> GetAllAsync(string[] includes);
       Task <IEnumerable<T>> GetActiveAsync(string[] includes);
       Task <bool> AddAsync(T item);       
       Task <bool> SetPassiveAsync(Guid id);
       Task <bool> SetPassiveAsync(Expression<Func<T, bool>> exp);
       Task <bool> RemoveAsync(T item);
       Task <bool> ActivateAsync(Guid id);
       Task <bool> UpdateAsync(T item);
       Task <bool> UpdateAsync(IEnumerable<T> items);
       Task <bool> Save();
        IEnumerable<T> Where(Expression<Func<T, bool>> where);
    }
}
