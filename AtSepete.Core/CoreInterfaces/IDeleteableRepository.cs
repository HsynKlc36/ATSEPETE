using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Core.CoreInterfaces
{
    public interface IDeleteableRepository<T>:IRepository where T : Base
    {
        bool Delete(T entity);
    }
}
