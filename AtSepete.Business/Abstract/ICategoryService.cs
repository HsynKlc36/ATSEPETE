using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
using System.Linq.Expressions;

namespace AtSepete.Business.Abstract
{
    public interface ICategoryService:IGenericService<CategoryDto,Category>
    {   
        Task<BaseResponse<bool>> AddAsync(CategoryDto item);
        Task<BaseResponse<bool>> UpdateAsync(CategoryDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<CategoryDto> items);
        Task<BaseResponse<CategoryDto>> GetByIdentityAsync(string Identity);
        Task<BaseResponse<CategoryDto>> GetByDateAsync(DateTime date);
        Task<BaseResponse<IEnumerable<CategoryDto>>> GetIdentityAsync(string Identity);
        Task<BaseResponse<IEnumerable<CategoryDto>>> GetDateAsync(DateTime date);
        Task<BaseResponse<bool>> SetPassiveAsync(string Identity);
        Task<BaseResponse<bool>> SetPassiveAsync(DateTime date);
    }
}
