using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;

namespace AtSepete.Business.Abstract
{
    public interface ICategoryService:IGenericService<CategoryDto,Category>
    {   
        Task<BaseResponse<bool>> AddAsync(CategoryDto item);
        Task<BaseResponse<bool>> UpdateAsync(CategoryDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<CategoryDto> items);
    }
}
