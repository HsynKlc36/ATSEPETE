using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IOrderService 
    {
        Task<IDataResult<List<OrderListDto>>> GetAllOrderAsync();
        Task<IDataResult<OrderDto>> GetByIdOrderAsync(Guid id);
        Task<IDataResult<CreateOrderDto>> AddOrderAsync(CreateOrderDto entity);
        Task<IDataResult<UpdateOrderDto>> UpdateOrderAsync(Guid id, UpdateOrderDto updateOrderDto);
        Task<IResult> HardDeleteOrderAsync(Guid id);
        Task<IResult> SoftDeleteOrderAsync(Guid id);
       
    }
}
