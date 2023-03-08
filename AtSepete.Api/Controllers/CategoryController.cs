using AtSepete.Business.Abstract;
using AtSepete.Entities.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericService<Category> _categoryService;

        public CategoryController(IGenericService<Category> CategoryService)
        {
            _categoryService = CategoryService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCategory([FromBody]Category Category)
        {
            return Ok(await _categoryService.Add(Category));
        }
    }
}
