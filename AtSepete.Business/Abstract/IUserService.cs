using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IUserService : IGenericService<UserDto, User>
    {
        Task<BaseResponse<bool>> AddAsync(UserDto item);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, UserDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<UserDto> items);
    }
}
