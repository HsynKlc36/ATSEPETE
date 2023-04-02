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
    public interface IOrderDetailService
    {
        Task<IDataResult<List<OrderDetailListDto>>> GetAllOrderDetailAsync();

        Task<IDataResult<OrderDetailDto>> GetByIdOrderDetailAsync(Guid id);
        Task<IDataResult<CreateOrderDetailDto>> AddOrderDetailAsync(CreateOrderDetailDto entity);
        Task<IDataResult<UpdateOrderDetailDto>> UpdateOrderDetailAsync(Guid id, UpdateOrderDetailDto updateOrderDetailDto);
        Task<IResult> HardDeleteOrderDetailAsync(Guid id);
        Task<IResult> SoftDeleteOrderDetailAsync(Guid id);
        
    }
}
