using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Core.CoreInterfaces
{
    public interface IAsyncDeleteableRepository<T>:IAsyncRepository where T : Base
    {
        Task DeleteAsync(T entity);
    }
}
