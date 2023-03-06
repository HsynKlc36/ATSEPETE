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
       Task <T> GetById(Guid id);
       Task <T> GetByDefault(Expression<Func<T, bool>> exp);
       Task <IEnumerable<T>> GetDefault(Expression<Func<T, bool>> exp);
       Task <IEnumerable<T>> GetAll();
       Task <IEnumerable<T>> GetAll(string[] includes);
       Task <IEnumerable<T>> GetActive(string[] includes);
       Task <bool> Add(T item);       
       Task <bool> SetPassive(Guid id);
       Task <bool> SetPassive(Expression<Func<T, bool>> exp);
       Task <bool> Remove(T item);
       Task <bool> Activate(Guid id);
       Task <bool> Update(T item);
       Task <bool> Update(IEnumerable<T> items);
       Task <bool> Save();
    }
}
