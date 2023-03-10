using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IOrderService<OrderDto,Order>
    {
        Task<BaseResponse<OrderDto>> GetById(Guid id);
        Task<BaseResponse<OrderDto>> GetByDefault(Expression<Func<OrderDto, bool>> exp);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetDefault(Expression<Func<OrderDto, bool>> exp);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAll();
        Task<BaseResponse<bool>> Add(OrderDto item);
        Task<BaseResponse<bool>> SetPassive(Guid id);
        Task<BaseResponse<bool>> SetPassive(Expression<Func<OrderDto, bool>> exp);
        Task<BaseResponse<bool>> Remove(OrderDto item);
        Task<BaseResponse<bool>> Activate(Guid id);
        Task<BaseResponse<bool>> Update(OrderDto item);
        Task<BaseResponse<bool>> Update(IEnumerable<OrderDto> items);
    }
}
