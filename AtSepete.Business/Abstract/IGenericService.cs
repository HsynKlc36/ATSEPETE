
using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IGenericService<Dto,T> 
    {
        Task<BaseResponse<Dto>> GetByIdAsync(Guid id);
        Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync();
        Task<BaseResponse<bool>> SetPassiveAsync(Guid id);
        Task<BaseResponse<bool>> RemoveAsync(Guid id);
        Task<BaseResponse<bool>> ActivateAsync(Guid id);

     
    }
}
