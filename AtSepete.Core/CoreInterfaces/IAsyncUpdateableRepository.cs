using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Core.CoreInterfaces
{
    public interface IAsyncUpdateableRepository<T>:IAsyncRepository where T : Base
    {
        Task<T> UpdateAsync(T entity);
    }
}
