using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketService _productService;

        public MarketController(IMarketService marketService)
        {
            _productService = marketService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<MarketListDto>>> GetAllMarket()
        {
            return await _productService.GetAllMarketAsync();
        }
        [HttpGet]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<MarketDto>> GetByIdMarket([FromRoute] Guid id)
        {
            return await _productService.GetByIdMarketAsync(id); ;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateMarketDto>> AddMarket([FromBody] CreateMarketDto createMarketDto)
        {
            return await _productService.AddMarketAsync(createMarketDto);
        }
        [HttpPut]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<UpdateMarketDto>> UpdateMarket([FromRoute] Guid id, [FromBody] UpdateMarketDto updateMarketDto)
        {
            return await _productService.UpdateMarketAsync(id, updateMarketDto); ;
        }
        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> HardDeleteMarket([FromRoute] Guid id)
        {
            return await _productService.HardDeleteMarketAsync(id);
        }
        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> SoftDeleteMarket([FromRoute] Guid id)
        {
            return await _productService.SoftDeleteMarketAsync(id);
        }
    }
}
