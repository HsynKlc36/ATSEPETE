using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.UI.ApiResponses.CustomerOrderApiResponse;
using AtSepete.UI.ApiResponses.ShopApiResponse;
using AtSepete.UI.Areas.Customer.Models.CustomerOrderVMs;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    public class CustomerOrderController : CustomerBaseController
    {
        private readonly IMapper _mapper;

        public CustomerOrderController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> CustomerOrderPage()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/CustomerOrder/CustomerOrderList/{UserId}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    CustomerOrderListResponse customerOrderList = JsonConvert.DeserializeObject<CustomerOrderListResponse>(apiResponse);

                    if (customerOrderList.IsSuccess)
                    {
                        var customerOrders = _mapper.Map<List<CustomerOrderListDto>, List<CustomerCustomerOrderListVM>>(customerOrderList.Data);
                        NotifySuccessLocalized(customerOrderList.Message);
                        return View(customerOrders);

                    }
                    else
                    {
                        NotifyErrorLocalized(customerOrderList.Message);
                        return RedirectToAction("HomePage", "Shop");
                    }

                };



            }
        }
    }
}
