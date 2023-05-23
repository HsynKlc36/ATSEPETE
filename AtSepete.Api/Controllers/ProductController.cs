using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<ProductListDto>>> GetAllProduct()
        {
            return await _productService.GetAllProductAsync();
        }
        [HttpGet]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<ProductDto>> GetByIdProduct([FromRoute] Guid id)
        {
            return await _productService.GetByIdProductAsync(id); ;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateProductDto>> AddProduct([FromBody] CreateProductDto createProductDto)
        {
        
            return await _productService.AddProductAsync(createProductDto);
        }
        [HttpPut]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<UpdateProductDto>> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            return await _productService.UpdateProductAsync(id, updateProductDto); ;
        }
        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> HardDeleteProduct([FromRoute] Guid id)
        {
            return await _productService.HardDeleteProductAsync(id);
        }
        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> SoftDeleteProduct([FromRoute] Guid id)
        {
            return await _productService.SoftDeleteProductAsync(id);
        }

    }
}
