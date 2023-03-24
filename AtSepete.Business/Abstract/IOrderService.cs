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
        //Task<IDataResult<List<OrderDto>>> GetAllAsync();
        //Task<BaseResponse<Dto>> GetByIdAsync(Guid id);
        //Task<BaseResponse<bool>> SetPassiveAsync(Guid id);
        //Task<BaseResponse<bool>> RemoveAsync(Guid id);
        //Task<BaseResponse<bool>> ActivateAsync(Guid id);
        //Task<BaseResponse<bool>> AddAsync(CategoryDto item);
        //Task<BaseResponse<bool>> UpdateAsync(Guid id,CategoryDto item);
        //Task<BaseResponse<bool>> UpdateAsync(IEnumerable<CategoryDto> items);
        //Task<BaseResponse<CategoryDto>> GetByIdentityAsync(string Identity);
        //Task<BaseResponse<CategoryDto>> GetByDateAsync(DateTime date);
        //Task<BaseResponse<IEnumerable<CategoryDto>>> GetIdentityAsync(string Identity);
        //Task<BaseResponse<IEnumerable<CategoryDto>>> GetDateAsync(DateTime date);
        //Task<BaseResponse<bool>> SetPassiveAsync(string Identity);
        //Task<BaseResponse<bool>> SetPassiveAsync(DateTime date);
    }
}
