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
    public interface IOrderDetailService : IGenericService<OrderDetailDto, OrderDetail>
    {
        Task<BaseResponse<bool>> AddAsync(OrderDetailDto item);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, OrderDetailDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<OrderDetailDto> items);
    }
}
