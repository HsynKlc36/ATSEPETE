using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IOrderDetailService<OrderDetailDto,OrderDetail>
    {
        Task<BaseResponse<OrderDetailDto>> GetById(Guid id);
        Task<BaseResponse<OrderDetailDto>> GetByDefault(Expression<Func<OrderDetailDto, bool>> exp);
        Task<BaseResponse<IEnumerable<OrderDetailDto>>> GetDefault(Expression<Func<OrderDetailDto, bool>> exp);
        Task<BaseResponse<IEnumerable<OrderDetailDto>>> GetAll();
        Task<BaseResponse<bool>> Add(OrderDetailDto item);
        Task<BaseResponse<bool>> SetPassive(Guid id);
        Task<BaseResponse<bool>> SetPassive(Expression<Func<OrderDetailDto, bool>> exp);
        Task<BaseResponse<bool>> Remove(OrderDetailDto item);
        Task<BaseResponse<bool>> Activate(Guid id);
        Task<BaseResponse<bool>> Update(OrderDetailDto item);
        Task<BaseResponse<bool>> Update(IEnumerable<OrderDetailDto> items);
    }
}
