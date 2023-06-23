using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.Results;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;


        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;

        }
        [HttpGet]
        [Route("[action]/{customerId}")]
        public async Task<IDataResult<List<CustomerOrderListDto>>> CustomerOrderList([FromRoute]Guid customerId)
        {
                return await _customerOrderService.CustomerOrdersAsync(customerId);
        }
    }
}
