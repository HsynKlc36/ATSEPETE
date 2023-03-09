using AtSepete.Entities.Base;
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
        Task<BaseResponse<Dto>> GetById(Guid id);
        Task<BaseResponse<Dto>> GetByDefault(Expression<Func<Dto, bool>> exp);
        Task<BaseResponse<IEnumerable<Dto>>> GetDefault(Expression<Func<Dto, bool>> exp);
        Task<BaseResponse<IEnumerable<Dto>>> GetAll();
        Task<BaseResponse<bool>> Add(Dto item);
        Task<BaseResponse<bool>> SetPassive(Guid id);
        Task<BaseResponse<bool>> SetPassive(Expression<Func<Dto, bool>> exp);
        Task<BaseResponse<bool>> Remove(Dto item);
        Task<BaseResponse<bool>> Activate(Guid id);
        Task<BaseResponse<bool>> Update(Dto item);
        Task<BaseResponse<bool>> Update(IEnumerable<Dto> items);
        //Task<BaseResponse<IEnumerable<Dto>>> GetAll(string[] includes);
        //Task<BaseResponse<IEnumerable<Dto>>> GetActive(string[] includes);
    }
}
