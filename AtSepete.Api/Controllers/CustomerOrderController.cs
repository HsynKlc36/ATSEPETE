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
        private readonly ISendOrderMessageService _sendOrderMessageService;

        public CustomerOrderController(ICustomerOrderService customerOrderService,ISendOrderMessageService sendOrderMessageService)
        {
            _customerOrderService = customerOrderService;
            _sendOrderMessageService = sendOrderMessageService;
        }
        [HttpGet]
        [Route("[action]/{customerId}")]
        public async Task<IDataResult<List<CustomerOrderListDto>>> CustomerOrderList([FromRoute]Guid customerId)
        {
            var customerOrders= await _customerOrderService.CustomerOrdersAsync(customerId);
            if (customerOrders.IsSuccess)
            {
                // Siparişleri almak için başarılı sonuç
                var orderIds = customerOrders.Data.Select(o => o.OrderId).ToList();

                // SendOrderMessageService'i kullanarak ExecuteAsync metodunu çağır
                await _sendOrderMessageService.GetOrders(CancellationToken.None, orderIds);

                // Siparişleri dön
                return customerOrders;
            }
           
                return customerOrders;
            
        }
    }
}
