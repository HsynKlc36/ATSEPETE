using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Results;
using System.Linq.Expressions;

namespace AtSepete.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<List<CategoryListDto>>> GetAllCategoryAsync();
        Task<IDataResult<CategoryDto>> GetByIdCategoryAsync(Guid id);
        Task<IDataResult<CreateCategoryDto>> AddCategoryAsync(CreateCategoryDto entity);
        Task<IDataResult<UpdateCategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto);
        Task<IResult> HardDeleteCategoryAsync(Guid id);
        Task<IResult> SoftDeleteCategoryAsync(Guid id);

    }
}
