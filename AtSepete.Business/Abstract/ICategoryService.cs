using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Results;
using System.Linq.Expressions;

namespace AtSepete.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<List<CategoryDto>>> GetAllAsync();
        Task<IDataResult<CategoryDto>> GetByDefaultAsync(Guid id);
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
