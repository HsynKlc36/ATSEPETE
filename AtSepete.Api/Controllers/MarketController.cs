using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketService _marketService;

        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateMarketDto>> AddMarket([FromBody] CreateMarketDto createMarketDto)
        {
            return await _marketService.AddMarketAsync(createMarketDto);
        }
    }
}
