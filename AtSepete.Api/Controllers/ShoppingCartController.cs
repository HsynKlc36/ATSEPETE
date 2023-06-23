using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
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
        private readonly ISendOrderMessageService _sendOrderMessageService;

        public ShoppingCartController(ICartService cartService,ISendOrderMessageService sendOrderMessageService)
        {
            _cartService = cartService;
            _sendOrderMessageService = sendOrderMessageService;
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IResult> CreateOrderList(List<CreateShoppingCartDto> createShoppingCarts)
        {
              var createdOrders= await _cartService.AddOrderAndOrderDetailAsync(createShoppingCarts);
            if (createdOrders.IsSuccess)
            {
                await _sendOrderMessageService.GetCreatedOrders($"{DateTime.Now.ToString()} Tarihli Yeni Sipariş!");//sipariş oluştuğunda dolacak ve tetiklenecektir
                return createdOrders;
            }
            return createdOrders;

        }
     
       
    }
}
