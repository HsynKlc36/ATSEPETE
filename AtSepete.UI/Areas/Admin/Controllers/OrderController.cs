using AtSepete.Dtos.Dto.Markets;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.UI.ApiResponses.MarketApiResponse;
using AtSepete.UI.ApiResponses.OrderApiResponse;
using AtSepete.UI.Areas.Admin.Models.MarketVMs;
using AtSepete.UI.Areas.Admin.Models.OrderVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class OrderController : AdminBaseController
    {
        private readonly IMapper _mapper;

        public OrderController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;

        }
        public async Task<IActionResult> OrderList()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Order/GetAllOrder"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    OrderListResponse orderList = JsonConvert.DeserializeObject<OrderListResponse>(apiResponse);
                    if (orderList.IsSuccess)
                    {
                        var orders = _mapper.Map<List<OrderListDto>, List<AdminOrderListVM>>(orderList.Data);
                        NotifySuccess(orderList.Message);
                        return View(orders);
                    }
                    else
                    {
                        NotifyError(orderList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }
    }
}
