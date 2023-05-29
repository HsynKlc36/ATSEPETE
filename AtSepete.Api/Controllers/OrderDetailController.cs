using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<OrderDetailListDto>>> GetAllOrderDetails()
        {

            return await _orderDetailService.GetAllOrderDetailAsync();
        }


        [HttpGet]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<OrderDetailDto>> GetByIdOrderDetail([FromRoute] Guid id)
        {
            
            return await _orderDetailService.GetOrderDetailWithNames(id);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateOrderDetailDto>> AddOrderDetail([FromBody] CreateOrderDetailDto createOrderDetailDto)
        {
            return await _orderDetailService.AddOrderDetailAsync(createOrderDetailDto);
        }

        [HttpPut]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<UpdateOrderDetailDto>> UpdateOrderDetail([FromRoute] Guid id, [FromBody] UpdateOrderDetailDto updateOrderDetail)
        {
            return await _orderDetailService.UpdateOrderDetailAsync(id, updateOrderDetail); ;
        }

        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> HardDeleteOrderDetail([FromRoute] Guid id)
        {
            return await _orderDetailService.HardDeleteOrderDetailAsync(id);
        }

        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> SoftDeleteOrderDetail([FromRoute] Guid id)
        {
            return await _orderDetailService.SoftDeleteOrderDetailAsync(id);
        }

    }
}
