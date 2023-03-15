using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
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
        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategory()
        {
            return await _categoryService.GetAllAsync();
        }
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> CreateCategory([FromBody]CategoryDto Category)
        //{
        //    return Ok(await _categoryService.AddAsync(Category));
        //}
    }
}
