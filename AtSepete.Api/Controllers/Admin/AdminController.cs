using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers.Admin
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AdminController : ControllerBase
    {
        public AdminController()
        {

        }
        //Category işlemleri
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IDataResult<CreateCategoryDto>> AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        //{
        //    return await _categoryService.AddCategoryAsync(createCategoryDto);
        //}
        //[HttpPut]
        //[Route("[action]/{id:Guid}")]
        //public async Task<IDataResult<UpdateCategoryDto>> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
        //{
        //    return await _categoryService.UpdateCategoryAsync(id, updateCategoryDto); ;
        //}
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> HardDeleteCategory([FromRoute] Guid id)
        //{
        //    return await _categoryService.HardDeleteCategoryAsync(id);
        //}
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> SoftDeleteCategory([FromRoute] Guid id)
        //{
        //    return await _categoryService.SoftDeleteCategoryAsync(id);
        //}

        ////Market işlemleri
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IDataResult<CreateMarketDto>> AddMarket([FromBody] CreateMarketDto createMarketDto)
        //{
        //    return await _productService.AddMarketAsync(createMarketDto);
        //}
        //[HttpPut]
        //[Route("[action]/{id:Guid}")]
        //public async Task<IDataResult<UpdateMarketDto>> UpdateMarket([FromRoute] Guid id, [FromBody] UpdateMarketDto updateMarketDto)
        //{
        //    return await _productService.UpdateMarketAsync(id, updateMarketDto); ;
        //}
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> HardDeleteMarket([FromRoute] Guid id)
        //{
        //    return await _productService.HardDeleteMarketAsync(id);
        //}
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> SoftDeleteMarket([FromRoute] Guid id)
        //{
        //    return await _productService.SoftDeleteMarketAsync(id);
        //}
        ////order işlemleri
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> HardDeleteOrder([FromRoute] Guid id)
        //{
        //    return await _orderService.HardDeleteOrderAsync(id);
        //}

        ////orderDetail İşlemleri
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> HardDeleteOrderDetail([FromRoute] Guid id)
        //{
        //    return await _orderDetailService.HardDeleteOrderDetailAsync(id);
        //}
        ////Product İşlemleri
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IDataResult<CreateProductDto>> AddProduct([FromBody] CreateProductDto createProductDto)
        //{
        //    return await _productService.AddProductAsync(createProductDto);
        //}
        //[HttpPut]
        //[Route("[action]/{id:Guid}")]
        //public async Task<IDataResult<UpdateProductDto>> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDto updateProductDto)
        //{
        //    return await _productService.UpdateProductAsync(id, updateProductDto); ;
        //}
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> HardDeleteProduct([FromRoute] Guid id)
        //{
        //    return await _productService.HardDeleteProductAsync(id);
        //}
        //[HttpDelete]
        //[Route("[Action]/{id:Guid}")]
        //public async Task<IResult> SoftDeleteProduct([FromRoute] Guid id)
        //{
        //    return await _productService.SoftDeleteProductAsync(id);
        //}

    }
}
