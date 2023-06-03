using AtSepete.Dtos.Dto.Products;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.UI.ApiResponses.ProductResponse;
using AtSepete.UI.ApiResponses.ShopApiResponse;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    public class ShopController : CustomerBaseController
    {
        private readonly IMapper _mapper;

        public ShopController(IToastNotification toastNotification, IConfiguration configuration,IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> HomePage()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/ShopList"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ShopListResponse shopList = JsonConvert.DeserializeObject<ShopListResponse>(apiResponse);
                    if (shopList.IsSuccess)
                    {
                        var Products = _mapper.Map<List<ShopListDto>, List<CustomerShopListVM>>(shopList.Data);
                        NotifySuccess(shopList.Message);
                        return View(Products);
                    }
                    else
                    {
                        NotifyError(shopList.Message);
                        return RedirectToAction("Index", "Home", new {area=""});
                    }
                };

            }




            return View();
        }
    }
}
