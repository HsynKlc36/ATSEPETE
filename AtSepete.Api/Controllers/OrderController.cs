using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<OrderListDto>>> GetAllOrder()
        {
            return await _orderService.GetAllOrderAsync();
        }
        [HttpGet]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<OrderDto>> GetByIdOrder([FromRoute] Guid id)
        {
            return await _orderService.GetByIdOrderAsync(id); ;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateOrderDto>> AddOrder([FromBody] CreateOrderDto createOrderDto)
        {
            return await _orderService.AddOrderAsync(createOrderDto);
        }
        [HttpPut]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<UpdateOrderDto>> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            return await _orderService.UpdateOrderAsync(id, updateOrderDto); ;
        }
        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> HardDeleteOrder([FromRoute] Guid id)
        {
            return await _orderService.HardDeleteOrderAsync(id);
        }
        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> SoftDeleteOrder([FromRoute] Guid id)
        {
            return await _orderService.SoftDeleteOrderAsync(id);
        }
    }
}
