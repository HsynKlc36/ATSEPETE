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
    public interface IOrderService : IGenericService<OrderDto, Order>
    {
        Task<BaseResponse<bool>> AddAsync(OrderDto item);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, OrderDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<OrderDto> items);
    }
}
