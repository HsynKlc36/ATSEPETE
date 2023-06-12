using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {

        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IDataResult<List<ShopListDto>>> ShopList()
        {
            return await _shopService.ShopListAsync();
        }
        [HttpGet]
        [Route("[action]/{productId}")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IDataResult<List<ShopProductDetailDto>>> ShopProductDetails([FromRoute]Guid productId)
        {
            return await _shopService.ShopProductDetailAsync(productId);
        }
        [HttpGet]
        [Route("[action]/{filterName}")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IDataResult<List<ShopFilterListDto>>> ShopFilterList([FromRoute]string filterName)
        {
            return await _shopService.ShopFilterListAsync(filterName);
        }
        [HttpGet]
        [Route("[action]/{sideBarFilter}")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IDataResult<List<ShopSideBarFilterListDto>>> ShopSideBarFilterList([FromRoute] string sideBarFilter)
        {
            return await _shopService.ShopSideBarFilterListAsync(sideBarFilter);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IDataResult<List<BestSellerProductListDto>>> BestSellerProductList()
        {
            return await _shopService.BestSellerProductsAsync();
        }
    }
}
