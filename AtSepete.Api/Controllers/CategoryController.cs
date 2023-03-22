using AtSepete.Api.Jwt;
using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
 

        public CategoryController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<CategoryListDto>>> GetAllCategory()
        {           
            return await _categoryService.GetAllCategoryAsync();
        }
        [HttpGet]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<CategoryDto>> GetByIdCategory([FromRoute]Guid id)
        {
            return await _categoryService.GetByIdCategoryAsync(id); ;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateCategoryDto>> AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            return await _categoryService.AddCategoryAsync(createCategoryDto);
        }
        [HttpPut]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<UpdateCategoryDto>> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            return await _categoryService.UpdateCategoryAsync(id, updateCategoryDto); ;
        }

    }
}
