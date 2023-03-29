using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Dtos.Dto;
using AtSepete.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class ProductMarketController : ControllerBase
    {
    
        private readonly IProductMarketService _productMarketService;

        public ProductMarketController(IProductMarketService productMarketService)
        {
           
            _productMarketService = productMarketService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<ProductMarketListDto>>>GetAllProductMarkets()
        {

            return await _productMarketService.GetAllProductMarketAsync();
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<ProductMarketListDto>>> GetAllProductMarkets()
        {

            return await _productMarketService.GetAllProductMarketAsync();
        }


        [HttpGet]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<ProductMarketDto>> GetByIdProductMarket([FromRoute] Guid id)
        {
            return await _productMarketService.GetByIdProductMarketAsync(id);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateProductMarketDto>> AddProductMarket([FromBody] CreateProductMarketDto createMarketProductDto)
        {
            return await _productMarketService.AddProductMarketAsync(createMarketProductDto);
        }

        [HttpPut]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<UpdateProductMarketDto>> UpdateProductMarket([FromRoute] Guid id, [FromBody] UpdateProductMarketDto updateProductMarket)
        {
            return await _productMarketService.UpdateProductMarketAsync(id, updateProductMarket); ;
        }

        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> HardDeleteProductMarket([FromRoute] Guid id)
        {
            return await _productMarketService.HardDeleteProductMarketAsync(id);
        }

        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> SoftDeleteCategory([FromRoute] Guid id)
        {
            return await _productMarketService.SoftDeleteProductMarketAsync(id);
        }




    }
}
