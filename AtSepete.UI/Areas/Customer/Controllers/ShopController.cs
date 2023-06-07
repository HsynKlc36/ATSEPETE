using AtSepete.Dtos.Dto.Products;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.Entities.Data;
using AtSepete.UI.ApiResponses.ProductResponse;
using AtSepete.UI.ApiResponses.ShopApiResponse;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using SendGrid;
using System.Net;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    public class ShopController : CustomerBaseController
    {
        private readonly IMapper _mapper;

        public ShopController(IToastNotification toastNotification, IConfiguration configuration, IMapper mapper) : base(toastNotification, configuration)
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
                        var products = _mapper.Map<List<ShopListDto>, List<CustomerShopListVM>>(shopList.Data);
                        NotifySuccess(shopList.Message);
                        ViewBag.ShopList = products;

                    }
                    else
                    {
                        NotifyError(shopList.Message);
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }

                };
                using (HttpResponseMessage responseBestSeller = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/BestSellerProductList"))
                {

                    if (responseBestSeller.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiBestSellerResponse = await responseBestSeller.Content.ReadAsStringAsync();
                    BestSellerListResponse bestSellerProductList = JsonConvert.DeserializeObject<BestSellerListResponse>(apiBestSellerResponse);

                    if (bestSellerProductList.IsSuccess)
                    {
                        var bestSellerProducts = _mapper.Map<List<BestSellerProductListDto>, List<CustomerBestSellerListVM>>(bestSellerProductList.Data);
                        NotifySuccess(bestSellerProductList.Message);
                        ViewBag.BestSellerProductList = bestSellerProducts;
                    }
                    else
                    {
                        NotifyError(bestSellerProductList.Message);
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                }

                return View();
            }
        }

        public async Task<IActionResult> HomePageFilter(string filterName)
        {
            if (string.IsNullOrEmpty(filterName))
            {
                return RedirectToAction("HomePage");
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/ShopFilterList/{filterName}"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ShopFilterListResponse shopFilterList = JsonConvert.DeserializeObject<ShopFilterListResponse>(apiResponse);

                    if (shopFilterList.IsSuccess)
                    {
                        var filterProducts = _mapper.Map<List<ShopFilterListDto>, List<CustomerShopFilterListVM>>(shopFilterList.Data);
                        NotifySuccess(shopFilterList.Message);
                        return View();
                    }
                    else
                    {
                        NotifyError(shopFilterList.Message);
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }

                };
            }


        }
    }
}
