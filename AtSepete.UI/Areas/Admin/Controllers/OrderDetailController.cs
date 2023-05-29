using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Dtos.Dto.Products;
using AtSepete.UI.ApiResponses.OrderApiResponse;
using AtSepete.UI.ApiResponses.OrderDetailApiResponse;
using AtSepete.UI.ApiResponses.ProductApiResponse;
using AtSepete.UI.Areas.Admin.Models.OrderDetailVMs;
using AtSepete.UI.Areas.Admin.Models.OrderVMs;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    public class OrderDetailController : AdminBaseController
    {
        private readonly IMapper _mapper;

        public OrderDetailController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> OrderDetailList()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/OrderDetail/GetAllOrderDetails"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    OrderDetailListResponse orderDetailList = JsonConvert.DeserializeObject<OrderDetailListResponse>(apiResponse);
                    if (orderDetailList.IsSuccess)
                    {
                        var orderDetails = _mapper.Map<List<OrderDetailListDto>, List<AdminOrderDetailListVM>>(orderDetailList.Data);
                        NotifySuccess(orderDetailList.Message);
                        return View(orderDetails);
                    }
                    else
                    {
                        NotifyError(orderDetailList.Message);
                        return RedirectToAction("Index", "Admin");
                    }
                };

            }
        }

        public async Task<IActionResult> OrderDetail(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/OrderDetail/GetByIdOrderDetail/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    OrderDetailResponse orderDetailResponse = JsonConvert.DeserializeObject<OrderDetailResponse>(apiResponse);
                    if (orderDetailResponse.IsSuccess)
                    {
                        var orderDetail = _mapper.Map<OrderDetailDto, AdminOrderDetailVM>(orderDetailResponse.Data);//data'ların response' den boş gelme ihtimalkeri de kontrol edilmeli
                        NotifySuccess(orderDetailResponse.Message);
                        return View(orderDetail);
                    }
                    else
                    {
                        NotifyError(orderDetailResponse.Message);
                        return RedirectToAction("OrderDetailList");
                    }
                };
            }
        }
    }
}
