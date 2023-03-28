using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Dtos.Dto;
using AtSepete.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateProductMarketDto>> AddProductMarket([FromBody] CreateProductMarketDto createMarketProductDto)
        {
            return await _productMarketService.AddProductMarketAsync(createMarketProductDto);
        }
    }
}
