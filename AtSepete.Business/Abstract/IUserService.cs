using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IUserService<UserDto,User>
    {
        Task<BaseResponse<UserDto>> GetById(Guid id);
        Task<BaseResponse<UserDto>> GetByDefault(Expression<Func<UserDto, bool>> exp);
        Task<BaseResponse<IEnumerable<UserDto>>> GetDefault(Expression<Func<UserDto, bool>> exp);
        Task<BaseResponse<IEnumerable<UserDto>>> GetAll();
        Task<BaseResponse<bool>> Add(UserDto item);
        Task<BaseResponse<bool>> SetPassive(Guid id);
        Task<BaseResponse<bool>> SetPassive(Expression<Func<UserDto, bool>> exp);
        Task<BaseResponse<bool>> Remove(UserDto item);
        Task<BaseResponse<bool>> Activate(Guid id);
        Task<BaseResponse<bool>> Update(UserDto item);
        Task<BaseResponse<bool>> Update(IEnumerable<UserDto> items);
    }
}
