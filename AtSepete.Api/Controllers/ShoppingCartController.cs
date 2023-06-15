using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto.Carts;
using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public ShoppingCartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IResult> CreateOrderList(List<CreateShoppingCartDto> createShoppingCarts)
        {
            return await _cartService.AddOrderAndOrderDetailAsync(createShoppingCarts);

        }
     
       
    }
}
